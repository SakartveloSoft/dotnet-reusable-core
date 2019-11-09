using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SakartveloSoft.API.Core.Logging;
using SakartveloSoft.API.Core.Services;
using SakartveloSoft.API.Framework.Adapters;

namespace TestWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    var minSeverityStr = (Environment.GetEnvironmentVariable("logging:minSeverity") ?? "debugging").Trim().ToLowerInvariant();
                    var minSeverity = LoggingSeverity.Debugging;
                    try
                    {
                        minSeverity = Enum.Parse<LoggingSeverity>(minSeverityStr, true);
                    }
                    catch
                    {

                    }
                    var loggingAdapter = new LoggingPlatformAdapter().SetMinSeverity(minSeverity);
                    webBuilder.ConfigureServices(collection =>
                    {
                        collection.AddSingleton<ILoggingServiceProxy>(loggingAdapter);
                    });
                    webBuilder.ConfigureLogging(logging =>
                    {
                        logging.AddFilter("Microsoft", LogLevel.Warning);
                        logging.AddFilter("System", LogLevel.Warning);
                        logging.ClearProviders();
                        logging.AddProvider(loggingAdapter);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
