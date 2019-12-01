using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Configuration
{
    public interface IConfigurationManipulator
    {
        public Task<IReadOnlyList<IConfigurationEntry>> GetEntries(string component, ConfigurationPath pathPrefix, bool forPages = false, bool recursive = true);
        public Task<IConfigurationEntry> GetEntry(string component, ConfigurationPath path);
        public Task<IConfigurationEntry> EnsureForEntry(string component, ConfigurationPath path, ConfigurationValue value = null, ConfigurationValueMeaning? meaning = null, string label = null, bool? visibleForPages = null);
        public Task<bool> DeleteEntry(string component, ConfigurationPath path);
    }
}
