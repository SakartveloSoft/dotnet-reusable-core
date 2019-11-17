using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Configuration
{
    public interface IConfigurationManipulator
    {
        public Task<IReadOnlyList<IConfigurationEntry>> GetEntries(string component, string pathPrefix, bool? forPages);
        public Task<IConfigurationEntry> GetEntry(string component, string path);
        public Task<IConfigurationEntry> EnsureForEntry(string component, string path, ConfigurationValue value, ConfigurationValueMeaning? meaning, string label, bool? clientVisibility);
        public Task<bool> DeleteEntry(string component, string path);
    }
}
