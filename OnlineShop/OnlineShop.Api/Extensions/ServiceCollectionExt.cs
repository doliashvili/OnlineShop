using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineShop.Infrastructure;
using OnlineShop.Infrastructure.CommonSql;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.OpenApi.Models;

namespace OnlineShop.Api.Extensions
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection ConfigureSerilog(this IServiceCollection self, IConfiguration configuration)
        {
            const string outputTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";

            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: outputTemplate, theme: AnsiConsoleTheme.Code)
                //.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day, buffered: true)
                .WriteTo.Async(a => a.File("logs/.log", outputTemplate: outputTemplate, rollingInterval: RollingInterval.Day))
                .MinimumLevel.Information()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            Log.Logger = logger;

            //services.AddSingleton<Microsoft.Extensions.Logging.ILogger>(Log.Logger);
            self.AddSingleton<ILoggerFactory>(_ => new SerilogLoggerFactory(null, true));

            return self;
        }

        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["Database:ConnectionString"];

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                Log.Fatal("Neither Database connection nor Api url was configured in appsettings.json");
                throw new ApplicationException("Neither Database connection nor Api url was configured in appsettings.json");
            }

            services.AddSingleton(_ => new DatabaseConnectionString(connectionString));

            Log.Information("Database connection string: '{ConnectionString}'", connectionString.MaskDbConnectionString());

            DbUpdater.CheckAndUpdateDatabaseVersion(connectionString);

            return services;
        }

        internal static void AddSwaggerXml(this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions c)
        {
            var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
            foreach (var xmlFile in xmlFiles)
            {
                c.IncludeXmlComments(xmlFile);
            }
        }

        internal static void AddSwaggerAuthorizeBearer(this Swashbuckle.AspNetCore.SwaggerGen.SwaggerGenOptions x)
        {
            x.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "OnlineShop Api"
            });
            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description =
                    "Input your Bearer token in this format - Bearer {your token here} to access this API",
            });

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    }, new List<string>()
                },
            });
        }
    }
}