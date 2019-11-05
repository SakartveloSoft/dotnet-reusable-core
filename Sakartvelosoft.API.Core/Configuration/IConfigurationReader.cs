using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Configuration
{
    public interface IConfigurationReader
    {
        object GetValue(string name, object fallback);
        T Get<T>(string name, T fallback = default(T));
        string this[string name] { get; }
        int GetInteger(string name, int fallback = 0);
        double GetDouble(string name, double fallback = 0);
        bool GetBoolean(string name, bool fallback = false);
        DateTime? GetDateTime(string name, DateTime? fallback = null);
    }
}
