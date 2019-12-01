using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Configuration
{
    public class ConfigurationEntry : IConfigurationEntry
    {
        public string Component { get; set; }
        public bool ComponentIsRoot {  get
            {
                return string.IsNullOrWhiteSpace(Component);
            } 
        }
        public ConfigurationPath Path { get; set; }
        public string Label { get; set; }
        public bool VisibleToPages { get; set; }
        public ConfigurationValueMeaning ValueMeaning { get; set; } 
        public ConfigurationValueType ValueType { get; set; }

        public ConfigurationValue Value { get; set; }
    }
}
