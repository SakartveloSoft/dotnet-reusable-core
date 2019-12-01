using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SakartveloSoft.Configuration.CosmosDB
{
    public class ConfigurationManipulator: IConfigurationManipulator
    {
        private MongoClient mongoClient = null;
        private IMongoDatabase settingsDatabase;
        private IMongoCollection<MongoDbConfigurationDocument> settingsCollectionData;
        private static readonly string RootComponentName = "root";

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string SettingsCollection { get; set; }
        public string EnvironmentId { get; set; }

        public Task<bool> DeleteEntry(string component, ConfigurationPath path)
        {
            throw new NotImplementedException();
        }

        public async Task<IConfigurationEntry> EnsureForEntry(string component, ConfigurationPath path, ConfigurationValue value, ConfigurationValueMeaning? meaning = null, string label = null, bool? visibleForPages = null)
        {
            var filterBuilder = new FilterDefinitionBuilder<MongoDbConfigurationDocument>();
            if (string.IsNullOrWhiteSpace(component))
            {
                component = RootComponentName;
            }
            var pathFilter = filterBuilder.Eq(a => a.Path, path.ToString()) & filterBuilder.Eq(a => a.Component, component);
            var entries = await settingsCollectionData.FindAsync(pathFilter);
            bool creatingNow = false;
            var entry = await entries.FirstOrDefaultAsync();
            if (entry == null)
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value), "Value is mandatory in EnsureForEntity operation when no configuration value found in database");
                }
                creatingNow = true;
                entry = new MongoDbConfigurationDocument
                {
                    Component = string.IsNullOrWhiteSpace(component) ? null : component,
                    Path = path,
                    ValueType = value.Type.ToString()
                };
            }
            if (meaning.HasValue)
            {
                entry.Meaning = meaning.Value.ToString();
            }
            if (label != null)
            {
                entry.Label = label;
            }
            if (visibleForPages.HasValue)
            {
                entry.VisibleToPages = visibleForPages.Value;
            }
            if (creatingNow)
            {
                ApplyValueToConfigurationEntry(entry, value);
                await settingsCollectionData.InsertOneAsync(entry);
            } else
            {
                var updateBuilder = new UpdateDefinitionBuilder<MongoDbConfigurationDocument>();
                List<UpdateDefinition<MongoDbConfigurationDocument>> updates = new List<UpdateDefinition<MongoDbConfigurationDocument>>();
                if (visibleForPages.HasValue)
                {
                    entry.VisibleToPages = visibleForPages.Value;
                    updates.Add(updateBuilder.Set(a => a.VisibleToPages, entry.VisibleToPages));
                }
                if (!string.IsNullOrWhiteSpace(label)) {
                    entry.Label = label;
                    updates.Add(updateBuilder.Set(a => a.Label, label));
                }
                if (meaning.HasValue)
                {
                    entry.Meaning = meaning.Value.ToString();
                    updates.Add(updateBuilder.Set(a => a.Meaning, meaning.ToString()));
                }
                if (value != null)
                {
                    ApplyValueToConfigurationEntry(entry, value);
                    updates.Add(updateBuilder.Set(a => a.ValueType, entry.ValueType));
                    updates.Add(updateBuilder.Set(a => a.StringValue, entry.StringValue));
                    updates.Add(updateBuilder.Set(a => a.BooleanValue, entry.BooleanValue));
                    updates.Add(updateBuilder.Set(a => a.LongValue, entry.LongValue));
                    updates.Add(updateBuilder.Set(a => a.DoubleValue, entry.DoubleValue));
                    updates.Add(updateBuilder.Set(a => a.BytesValue, entry.BytesValue));
                }
                var builder = new FilterDefinitionBuilder<MongoDbConfigurationDocument>();
                var filter = builder.Eq(a => a.Id, entry.Id);
                await settingsCollectionData.UpdateOneAsync(filter, updateBuilder.Combine(updates));
                foreach(var change in updates)
                {
                    
                }
            }
            return RebuildConfigurationEntry(entry);
        }

        private void ApplyValueToConfigurationEntry(MongoDbConfigurationDocument entry, ConfigurationValue value)
        {
            entry.ValueType = value.Type.ToString();
            switch(value.Type)
            {
                case ConfigurationValueType.Boolean:
                    entry.BooleanValue = value.BooleanValue;
                    break;
                case ConfigurationValueType.String:
                    entry.StringValue = value.StringValue;
                    break;
                case ConfigurationValueType.Integer:
                    entry.LongValue = value.IntegerValue;
                    break;
                case ConfigurationValueType.Double:
                    entry.DoubleValue = value.DoubleValue;
                    break;
                case ConfigurationValueType.DateTime:
                    entry.StringValue = value.DateTime.ToUniversalTime().ToString("u");
                    entry.LongValue = value.DateTime.Ticks;
                    break;
                case ConfigurationValueType.TimeSpan:
                    entry.StringValue = value.TimeSpan.ToString();
                    entry.LongValue = value.TimeSpan.Ticks;
                    break;
                case ConfigurationValueType.Bytes:
                    entry.BytesValue = value.BytesValue;
                    break;
                case ConfigurationValueType.Null:
                    break;
                case ConfigurationValueType.Json:
                case ConfigurationValueType.Object:
                    entry.StringValue = JsonConvert.SerializeObject(value.Value);
                    break;
                    
            }
        }

        public async Task<IReadOnlyList<IConfigurationEntry>> GetEntries(string component, ConfigurationPath pathPrefix, bool forPages = false, bool recursive=true)
        {
            var filterBuilder = new FilterDefinitionBuilder<MongoDbConfigurationDocument>();
            if (string.IsNullOrWhiteSpace(component))
            {
                component = "root";
            }
            var filter = filterBuilder.Eq(a => a.Component, component);
            filter = recursive ? filterBuilder.Regex(a => a.Path, new Regex(pathPrefix.ToString(asPrefix: true))) : filterBuilder.Eq(a => a.Path, pathPrefix.ToString());
            if (forPages)
            {
                filter = filter & filterBuilder.Eq(a => a.VisibleToPages, true);
            }

            var results = await settingsCollectionData.FindAsync(filter);
            var records = await results.ToListAsync();
            return records.Select(doc => RebuildConfigurationEntry(doc)).ToList();
        }

        private IConfigurationEntry RebuildConfigurationEntry(MongoDbConfigurationDocument doc)
        {
            var entry = new ConfigurationEntry
            {
                Component = doc.Component,
                Path = doc.Path,
                Label = doc.Label ?? doc.Path,
                ValueMeaning = Enum.TryParse(doc.Meaning, out ConfigurationValueMeaning meaning) ? meaning : ConfigurationValueMeaning.String,
                ValueType = Enum.TryParse(doc.ValueType, out ConfigurationValueType type) ? type : ConfigurationValueType.String,
                VisibleToPages = doc.VisibleToPages
            };
            switch(entry.ValueType)
            {
                case ConfigurationValueType.Boolean:
                    entry.Value = doc.BooleanValue;
                    break;
                case ConfigurationValueType.DateTime:
                    entry.Value = new DateTime(doc.LongValue.Value, DateTimeKind.Utc);
                    break;
                case ConfigurationValueType.Null:
                    entry.Value = ConfigurationValue.NullValue;
                    break;
                case ConfigurationValueType.String:
                    entry.Value = doc.StringValue;
                    break;
                case ConfigurationValueType.Integer:
                    entry.Value = doc.LongValue.GetValueOrDefault();
                    break;
                case ConfigurationValueType.Double:
                    entry.Value = doc.DoubleValue.GetValueOrDefault();
                    break;
                case ConfigurationValueType.TimeSpan:
                    entry.Value = TimeSpan.FromTicks(doc.LongValue.Value);
                    break;
                case ConfigurationValueType.Bytes:
                    entry.Value = doc.BytesValue;
                    break;
                case ConfigurationValueType.Json:
                    entry.Value = ConfigurationValue.FromJSON(doc.StringValue);
                    break;
                case ConfigurationValueType.Object:
                    throw new NotSupportedException("Raw objects loading does not supported in current version");
                default:
                    throw new NotImplementedException("Decoding from Mongo configuration document to Configuration value for arbitary types is not supported");
            }
            return entry;
        }

        public async Task<IConfigurationEntry> GetEntry(string component, ConfigurationPath path)
        {
            var filterBuilder = new FilterDefinitionBuilder<MongoDbConfigurationDocument>();
            var filter = filterBuilder.Eq(a => a.Path, path.ToString());
            if (string.IsNullOrWhiteSpace(component))
            {
                component = RootComponentName;
            }
            filter &= filterBuilder.Eq(a => a.Component, component);
            var entries = await settingsCollectionData.FindAsync(filter);
            var result = await entries.FirstOrDefaultAsync();
            return result == null ? default : RebuildConfigurationEntry(result);
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
            settingsCollectionData = settingsDatabase.GetCollection<MongoDbConfigurationDocument>(SettingsCollection);
            return await Task.FromResult(this);
        }

    }
}
