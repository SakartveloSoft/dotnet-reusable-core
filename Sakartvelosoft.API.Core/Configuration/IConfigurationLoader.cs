using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Configuration
{
    public interface IConfigurationLoader
    {
        string Name { get; }
        Task LoadValues(IDictionary<string, ConfigurationValue> values, bool forClient = false);
    }
}
