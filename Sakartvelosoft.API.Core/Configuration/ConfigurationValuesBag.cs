using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Configuration
{
    public class ConfigurationValuesBag : Dictionary<string, ConfigurationValue>, IConfigurationReader
    {
        string IConfigurationReader.this[string name] => this[name].StringValue;

        public T Get<T>(string name, T fallback = default)
        {
            return (T)this[name].Value;
        }

        public bool GetBoolean(string name, bool fallback = false)
        {
            return this[name].BooleanValue;
        }

        public DateTime? GetDateTime(string name, DateTime? fallback = null)
        {
            return this[name].DateTime;
        }

        public double GetDouble(string name, double fallback = 0)
        {
            return this[name].DoubleValue;
        }

        public int GetInteger(string name, int fallback = 0)
        {
            return this[name].IntegerValue;
        }

        public object GetValue(string name, object fallback)
        {
            return this[name].StringValue;
        }
    }
}
