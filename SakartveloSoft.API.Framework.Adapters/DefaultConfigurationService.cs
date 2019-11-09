using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SakartveloSoft.API.Core.Configuration;
using SakartveloSoft.API.Core.Services;

namespace SakartveloSoft.API.Framework.Adapters
{
    internal class DefaultConfigurationService: GlobalServiceBase, IConfigurationService
    {
        private IConfiguration configuration;
        public DefaultConfigurationService(IConfiguration configuration)
        {
            this.configuration = configuration;
            mergedConfiguration = new ConfigurationValuesBag();
            Configuration = mergedConfiguration;
        }

        private ConfigurationValuesBag mergedConfiguration;



        private List<IConfigurationLoader> loaders = new List<IConfigurationLoader>();

        public void AddProvider(IConfigurationLoader loader)
        {
            loaders.Add(loader);
        }

        protected override async Task PerformInitialization(IGlobalServicesContext context)
        {
            foreach(var loader in loaders)
            {
                await loader.LoadValues(mergedConfiguration);
            }
            this.Configuration = mergedConfiguration;
        }

    }
}