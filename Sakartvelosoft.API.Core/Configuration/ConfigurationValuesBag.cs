using SakartveloSoft.API.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Configuration
{
    public class ConfigurationValuesBag : Dictionary<string, ConfigurationValue>, IConfigurationReader, IExcludedFromLogging
    {


        string IConfigurationReader.this[string name]
        {
            get {
                if (TryGetValue(name, out ConfigurationValue val))
                {
                    return val.StringValue;
                }
                return "";
            }
        }

        public T Get<T>(string name, T fallback = default)
        {
            if (TryGetValue(name, out ConfigurationValue val))
            {
                return val.Value == null ? fallback : (T)val.Value;
            }
            else
            {
                return fallback;
            }
        }

        public bool GetBoolean(string name, bool fallback = false)
        {
            if (TryGetValue(name, out ConfigurationValue val))
            {
                return val.BooleanValue;
            }
            else
            {
                return fallback;
            }
        }

        public DateTime? GetDateTime(string name, DateTime? fallback = null)
        {
            if (TryGetValue(name, out ConfigurationValue dt))
            {
                return dt.DateTime;
            }
            else
            {
                return fallback;
            }
        }

        public double GetDouble(string name, double fallback = 0)
        {
            if (TryGetValue(name, out ConfigurationValue val))
            {
                return val.DoubleValue;
            }
            else
            {
                return fallback;
            }
        }

        public int GetInteger(string name, int fallback = 0)
        {
            if (TryGetValue(name, out ConfigurationValue val))
            {
                return val.IntegerValue;
            }
            else
            {
                return fallback;
            }
        }

        public object GetValue(string name, object fallback)
        {
            if (TryGetValue(name, out ConfigurationValue val))
            {
                return val.Value;
            }
            else
            {
                return fallback;
            }
        }
    }
}
