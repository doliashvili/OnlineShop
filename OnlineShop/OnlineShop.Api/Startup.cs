using System;
using System.Text;
using Cqrs.ApiGenerator;
using Cqrs.Extensions;
using Hangfire;
using Hangfire.MemoryStorage;
using Helpers.ReadResults;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using OnlineShop.Api.Extensions;
using OnlineShop.Application.CommandHandlers;
using OnlineShop.Application.Services.Abstract;
using OnlineShop.Application.Services.Implements;
using OnlineShop.Application.Settings;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.CommonModels.Identity;
using OnlineShop.Domain.Extensions;
using OnlineShop.Infrastructure.IdentityEF;
using OnlineShop.Infrastructure.Jobs;
using OnlineShop.Infrastructure.Repositories;
using StackExchange.Redis;

namespace OnlineShop.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureSerilog(_configuration)
                .ConfigureDatabase(_configuration);

            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));
            services.Configure<MailSettings>(_configuration.GetSection("MailSettings"));

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new ConsumesAttribute("application/json"));
                    options.Filters.Add(new ProducesAttribute("application/json"));
                    //options.Filters.Add<InputValidatorFilter>();
                    //options.Filters.Add<ResponseExceptionFilter>();
                })
                .AddJsonOptions(o => o.JsonSerializerOptions.SetDefaultJsonSerializerOptions());

            services.AddSwaggerGen(x =>
                {
                    x.AddSwaggerXml();
                    x.AddSwaggerAuthorizeBearer();
                })
                .AddSwaggerGenNewtonsoftSupport();

            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseSqlServer(_configuration["Database:ConnectionString"]);
            });

            //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(_configuration["RedisConnection"]));
            //services.AddScoped<ICartGuestService, CartGuestService>();

            // Add Hangfire services.
            services.AddHangfire(configuration =>
            {
                configuration.UseMemoryStorage();
            });

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            RegisterServices(services);
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<ICartWriteRepository, CartWriteRepository>();
            services.AddScoped<ICartReadRepository, CartReadRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IMailService, SmtpMailService>();

            services.AddMediator(new ApiGenOptions()
            {
                ControllersNamespace = "OnlineShop.Api.Controllers",
                ControllersPath = @"C:\Users\user\Source\Repos\OnlineShop\OnlineShop\OnlineShop.Api\Controllers",
                IgnoreGenerationIfControllerExists = true,
            },
                typeof(ProductCommandHandler).Assembly);

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                })
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultTokenProviders();

            services.Configure<JWTSettings>(_configuration.GetSection("JWTSettings"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = _configuration["JWTSettings:Issuer"],
                        ValidAudience = _configuration["JWTSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]))
                    };
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            c.NoResult();
                            c.Response.StatusCode = 500;
                            c.Response.ContentType = "text/plain";
                            return c.Response.WriteAsync(c.Exception.ToString());
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("You are not Authorized"));
                            return context.Response.WriteAsync(result);
                        },
                        OnForbidden = context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";
                            var result = JsonConvert.SerializeObject(Result.Fail("You have not permission to access this resource"));
                            return context.Response.WriteAsync(result);
                        },
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineShop Api"));

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();

            var path = app.ApplicationServices.GetRequiredService<IOptions<AppSettings>>().Value.StaticFilePath;

            RecurringJob.AddOrUpdate<ProductDeleteJob>(x => x.ExecuteAsync(), Cron.Weekly);
            RecurringJob.AddOrUpdate<DeleteImagesInFolder>(x => x.ExecuteAsync(path), Cron.Weekly);

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}