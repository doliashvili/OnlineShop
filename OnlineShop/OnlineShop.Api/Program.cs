using System;
using IdGeneration.GeneratorWrapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OnlineShop.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IdGenGlobalState.SetState(0, DateTimeOffset.Parse(DateTime.Now.ToShortDateString()));
            Console.WriteLine(IdGenerator.NewId);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
