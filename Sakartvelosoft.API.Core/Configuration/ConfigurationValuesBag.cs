using SakartveloSoft.API.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Configuration
{
    public class ConfigurationValuesBag : Dictionary<string, IConfigurationEntry>, IConfigurationReader, IExcludedFromLogging
    {


        string IConfigurationReader.this[string name]
        {
            get {
                if (TryGetValue(name, out IConfigurationEntry val))
                {
                    return (val.Value ?? ConfigurationValue.NullValue).StringValue;
                }
                return "";
            }
        }

        public T Get<T>(string name, T fallback = default)
        {
            if (TryGetValue(name, out IConfigurationEntry val))
            {
                var confValue = (val.Value ?? ConfigurationValue.NullValue).Value;
                return confValue ==  null ? fallback : (T)confValue;
            }
            else
            {
                return fallback;
            }
        }

        public bool GetBoolean(string name, bool fallback = false)
        {
            if (TryGetValue(name, out IConfigurationEntry val))
            {
                var confValue = (val.Value ?? ConfigurationValue.NullValue);
                return confValue.BooleanValue;
            }
            else
            {
                return fallback;
            }
        }

        public DateTime? GetDateTime(string name, DateTime? fallback = null)
        {
            if (TryGetValue(name, out IConfigurationEntry dt))
            {
                return (dt.Value ?? ConfigurationValue.NullValue).DateTime;
            }
            else
            {
                return fallback;
            }
        }

        public double GetDouble(string name, double fallback = 0)
        {
            if (TryGetValue(name, out IConfigurationEntry val))
            {
                return (val.Value ?? ConfigurationValue.NullValue).DoubleValue;
            }
            else
            {
                return fallback;
            }
        }

        public int GetInteger(string name, int fallback = 0)
        {
            if (TryGetValue(name, out IConfigurationEntry val))
            {
                return (val.Value ?? ConfigurationValue.NullValue).IntegerValue;
            }
            else
            {
                return fallback;
            }
        }

        public object GetValue(string name, object fallback)
        {
            if (TryGetValue(name, out IConfigurationEntry val))
            {
                return (val.Value ?? ConfigurationValue.NullValue).Value;
            }
            else
            {
                return fallback;
            }
        }
    }
}
