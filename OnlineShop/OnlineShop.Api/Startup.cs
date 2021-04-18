using ApiCommon.Filters;
using Cqrs.ApiGenerator;
using Cqrs.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineShop.Api.Extensions;
using OnlineShop.Application.CommandHandlers;
using OnlineShop.Application.Services;
using OnlineShop.Application.Settings;
using OnlineShop.Domain.AbstractRepository;
using OnlineShop.Domain.Extensions;
using OnlineShop.Infrastructure;

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

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new ConsumesAttribute("application/json"));
                    options.Filters.Add(new ProducesAttribute("application/json"));
                    options.Filters.Add<InputValidatorFilter>();
                    options.Filters.Add<ResponseExceptionFilter>();
                })
                .AddJsonOptions(o => o.JsonSerializerOptions.SetDefaultJsonSerializerOptions());

            services.AddSwaggerGen(x => x.AddSwaggerXml())
                .AddSwaggerGenNewtonsoftSupport();

            RegisterServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<IFileService, FileService>();
            services.AddMediator(new ApiGenOptions()
            {
                ControllersNamespace = "OnlineShop.Api.Controllers",
                ControllersPath = @"C:\OnlineShoping\OnlineShop\OnlineShop\OnlineShop.Api\Controllers",
                IgnoreGenerationIfControllerExists = true,
            },
                typeof(ProductCommandHandler).Assembly);
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

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
