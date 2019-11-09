using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class EnvVariablesConfigurationLoader : IConfigurationLoader
    {
        public string Name => "Environment Variables";

        public Task LoadValues(IDictionary<string, ConfigurationValue> values, bool forClient = false)
        {
            foreach(var env in Environment.GetEnvironmentVariables().Keys)
            {
                values[env.ToString()] = Environment.GetEnvironmentVariable(env.ToString()).ToString();
            }
            return Task.CompletedTask;
        }
    }
}
