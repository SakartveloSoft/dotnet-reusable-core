using MongoDB.Driver;
using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SakartveloSoft.Configuration.CosmosDB
{
    public class ConfigurationManipulator: IConfigurationManipulator
    {
        private MongoClient mongoClient = null;
        private IMongoDatabase settingsDatabase;
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string SettingsCollection { get; set; }
        public string EnvironmentId { get; set; }

        public Task<bool> DeleteEntry(string component, string path)
        {
            throw new NotImplementedException();
        }

        public Task<IConfigurationEntry> EnsureForEntry(string component, string path, ConfigurationValue value, ConfigurationValueMeaning? meaning, string label, bool? visibleForPages)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<IConfigurationEntry>> GetEntries(string component, string pathPrefix, bool? forPages)
        {
            throw new NotImplementedException();
        }

        public Task<IConfigurationEntry> GetEntry(string component, string path)
        {
            throw new NotImplementedException();
        }

        public async Task<ConfigurationManipulator> Initialize()
        {
            if (DatabaseName == null)
            {
                DatabaseName = @$"{EnvironmentId.ToLower()}Configuration";
            }
            if (SettingsCollection == null)
            {
                SettingsCollection = "settings";
            }
            mongoClient = new MongoClient(new MongoUrl(ConnectionString));
            settingsDatabase = mongoClient.GetDatabase(DatabaseName);
            return await Task.FromResult(this);
        }
    }
}
