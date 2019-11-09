using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SakartveloSoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using SakartveloSoft.API.Core.Configuration;
using SakartveloSoft.API.Core.Logging;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class DefaultGlobalContext : GlobalServiceBase, IGlobalServicesContext
    {
        public string EnvironmentId { get; set; }

        public string ApplicationId { get; set; }

        public string ServiceId { get; set; }

        public string VersionId { get; set; }

        public IReadOnlyDictionary<string, string> Environment { get; set; }

        private IDictionary<string, IGlobalService> globalServices = new Dictionary<string, IGlobalService>();
        private IConfiguration aspNetConfig;
        private IServiceCollection aspNetServices;

        public TService Get<TService>() where TService : class, IGlobalService
        {
            var serviceIntf = typeof(TService);
            if (globalServices.TryGetValue(serviceIntf.Name, out IGlobalService serv)) {
                if (serv is TService)
                {
                    return serv as TService;
                }
            }
            throw new ArgumentException("Service not found", serviceIntf.Name);
        }

        public bool Has<TService>() where TService : class, IGlobalService
        {
            var serviceIntf = typeof(TService);
            return globalServices.ContainsKey(serviceIntf.Name);
        }

        public IGlobalServicesContext AddGlobalService<TService>(TService service) where TService: class, IGlobalService
        {
            var serviceType = typeof(TService);
            if (globalServices.ContainsKey(serviceType.Name)) {
                throw new InvalidOperationException("Global service already registered");
            }
            globalServices.Add(serviceType.Name, service);
            aspNetServices.AddSingleton(service);
            return this;
        }

        public void UpdateGlobalOptions(string applicationName, string environmentName, string webRootPath, string contentRootPath)
        {
            this.ApplicationId = applicationName;
            this.EnvironmentId = environmentName;
            ContentRoot = contentRootPath;
            WebRoot = webRootPath;
        }

        protected override async Task PerformInitialization(IGlobalServicesContext context)
        {
            await Get<IConfigurationService>().Initialize(this);
            var conf = Get<IConfigurationService>();
            var log = Get<ILoggingService>();
            log.Configuration = conf.Configuration;
            await log.Initialize(this);
            foreach (var serviceEntry in globalServices)
            {
                if (!serviceEntry.Value.Ready)
                {
                    serviceEntry.Value.Configuration = conf.Configuration;
                    await serviceEntry.Value.Initialize(this);
                }
            }
        }

        public Task Initialize()
        {
            return Initialize(this);
        }

        public string ContentRoot { get; private set; }

        public string WebRoot { get; private set; }

        public DefaultGlobalContext(IServiceCollection aspNetServices, IConfiguration aspNetConfiguration)
        {
            aspNetConfig = aspNetConfiguration;
            this.aspNetServices = aspNetServices;
            var dict = new Dictionary<string, string>();
            var env = System.Environment.GetEnvironmentVariables();
            foreach(var name in env.Keys) 
            {
                dict[name.ToString()] = (env[name]).ToString();
            }
            this.Environment = dict;
            string val;
            if (Environment.TryGetValue("applicationId", out val))
            {
                this.ApplicationId = val;
            } else
            {
                this.ApplicationId = "";
            }
            if (Environment.TryGetValue("environmentId", out val))
            {
                this.EnvironmentId = val;
            }
            else
            {
                this.EnvironmentId = "unknown";
            }
            if (Environment.TryGetValue("serviceId", out val))
            {
                this.ServiceId = val;
            }
            else
            {
                this.ServiceId = "unknown";
            }
            if (Environment.TryGetValue("versionId", out val))
            {
                this.VersionId = val;
            }
            else
            {
                this.VersionId = "unknown";
            }
        }
    }
}
