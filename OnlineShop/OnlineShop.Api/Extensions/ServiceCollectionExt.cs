using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineShop.Infrastructure.CommonSql;
using OnlineShop.Infrastructure.DbUpdater;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.SystemConsole.Themes;

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
    }
}