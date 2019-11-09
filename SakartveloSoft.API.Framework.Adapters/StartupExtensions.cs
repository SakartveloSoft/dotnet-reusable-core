using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SakartveloSoft.API.Core.Configuration;
using SakartveloSoft.API.Core.Logging;
using SakartveloSoft.API.Core.Services;
using System;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public static class StartupExtensions
    {
        public static void AttachGlobalServices(this IServiceCollection services, IConfiguration configuration)
        {

            var globalContext = new DefaultGlobalContext(services, configuration);
            services.AddSingleton<IGlobalServicesContext>(globalContext);
            var confService = new DefaultConfigurationService(configuration);
            globalContext.AddGlobalService<IConfigurationService>(confService);
            confService.AddProvider(new EnvVariablesConfigurationLoader());
            services.AddSingleton(confService.Configuration);
            globalContext.AddGlobalService<ILoggingService>(new DefaultLoggingService());
        }

        public static async Task InitializeGlobalServices(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            var globalContext = app.ApplicationServices.GetService<IGlobalServicesContext>();
            globalContext.UpdateGlobalOptions(env.ApplicationName, env.EnvironmentName, env.WebRootPath, env.ContentRootPath);
            await globalContext.Initialize();
        }
    }
}
