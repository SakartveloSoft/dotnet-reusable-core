using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Configuration
{
    public interface IConfigurationEntry
    {
        public bool ComponentIsRoot { get; }
        public string Component { get; }
        public string Path { get; }
        public ConfigurationValueType ValueType { get; }
        public ConfigurationValueMeaning ValueMeaning { get; }
        public ConfigurationValue Value { get; }
    }
}
