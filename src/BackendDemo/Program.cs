using BackendDemo;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using BackendDemo.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BackendDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder()
              .UseUrls("http://*:5000")
              .ConfigureAppConfiguration(AppConfiguration)
              //.ConfigureLogging(LoggerConfiguration)
              .UseStartup<Startup>()
              .Build();

            host.ApplyMigrations()
                .Run();
        }

        static void AppConfiguration(WebHostBuilderContext context, IConfigurationBuilder config)
        {
            var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
            DirectoryInfo devSettingsFolder = currentDirectory;

            config
              .AddEnvironmentVariables()
              .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "config.json"));

        }


        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder()
              .ConfigureAppConfiguration((ctx, cfg) =>
              {
                  cfg
                  .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "config.json")) 
                  .AddEnvironmentVariables();
              })
              .ConfigureLogging((ctx, logging) => { }) // No logging
              .UseStartup<Startup>()
              .UseSetting("DesignTime", "true")
              .Build();
        }
    }


}
//        // read database configuration (database provider + database connection) from environment variables
//        var config = new ConfigurationBuilder()
//    .AddEnvironmentVariables()
//    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(),"config.json"))
//    .Build();

//var host = new WebHostBuilder()
//    .UseConfiguration(config)
//    .UseKestrel()
//    .UseUrls($"http://+:5000")
//    .UseStartup<Startup>()
//    .Build();

//await host.RunAsync();

