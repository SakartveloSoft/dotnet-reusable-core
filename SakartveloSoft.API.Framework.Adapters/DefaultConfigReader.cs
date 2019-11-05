using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class DefaultConfigReader : IConfigurationReader
    {
        private ConfigurationValuesBag values;

        public DefaultConfigReader(ConfigurationValuesBag values)
        {
            this.values = values;
        }

        public string this[string name]
        {
            get
            {
                if (values.TryGetValue(name, out ConfigurationValue val))
                {
                    return val.StringValue;
                }
                return "";
            }
        }

        public T Get<T>(string name, T fallback = default)
        {
            if (this.values.TryGetValue(name, out ConfigurationValue val))
            {
                return val.Value == null ? fallback : (T)val.Value;
            } else
            {
                return fallback;
            }
        }

        public bool GetBoolean(string name, bool fallback = false)
        {
            if (values.TryGetValue(name, out ConfigurationValue val))
            {
                return val.BooleanValue;
            } else
            {
                return fallback;
            }
        }

        public DateTime? GetDateTime(string name, DateTime? fallback = null)
        {
            if (values.TryGetValue(name, out ConfigurationValue dt))
            {
                return dt.DateTime;
            } else
            {
                return fallback;
            }
        }

        public double GetDouble(string name, double fallback = 0)
        {
            if (values.TryGetValue(name, out ConfigurationValue val))
            {
                return val.DoubleValue;
            } else
            {
                return fallback;
            }
        }

        public int GetInteger(string name, int fallback = 0)
        {
            if (values.TryGetValue(name, out ConfigurationValue val))
            {
                return val.IntegerValue;
            } else
            {
                return fallback;
            }
        }

        public object GetValue(string name, object fallback)
        {
            if (values.TryGetValue(name, out ConfigurationValue val))
            {
                return val.Value;
            } else
            {
                return fallback;
            }
        }
    }
}
