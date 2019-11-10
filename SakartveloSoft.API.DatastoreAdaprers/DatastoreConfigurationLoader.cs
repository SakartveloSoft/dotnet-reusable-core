using Google.Cloud.Datastore.V1;
using Newtonsoft.Json.Linq;
using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakartveloSoft.API.DatastoreAdaprers
{
    public class DatastoreConfigurationLoader : IConfigurationLoader
    {
        public string ProjectId { get; set; }
        public string ApplicationId { get; set; }
        public string ConfigurationNamespace { get; set; }
        public string EnvironmentId { get; set; }

        public DatastoreConfigurationLoader()
        {
            ProjectId = Environment.GetEnvironmentVariable("GCLOUD_PROJECT");
            if (string.IsNullOrEmpty(ProjectId))
            {
                ProjectId = Environment.GetEnvironmentVariable("project_id");
            }
            ApplicationId = Environment.GetEnvironmentVariable("application_id");
            EnvironmentId = Environment.GetEnvironmentVariable("environment_id");
            ConfigurationNamespace = Environment.GetEnvironmentVariable("configuration_namespace");
            if (string.IsNullOrEmpty(ConfigurationNamespace))
            {
                ConfigurationNamespace = @$"configuration_{ApplicationId}_{EnvironmentId}";
            }

        }

        public string Name => "Google DataStore";

        public ConfigurationValue ParseLodedConfigurationValue(Value confValue)
        {
            var parsedValue = ConfigurationValue.NullValue;
            switch (confValue.ValueTypeCase)
            {
                case Value.ValueTypeOneofCase.IntegerValue:
                    parsedValue = confValue.IntegerValue;
                    break;
                case Value.ValueTypeOneofCase.BooleanValue:
                    parsedValue = confValue.BooleanValue;
                    break;
                case Value.ValueTypeOneofCase.DoubleValue:
                    parsedValue = confValue.DoubleValue;
                    break;
                case Value.ValueTypeOneofCase.NullValue:
                    parsedValue = null;
                    break;
                case Value.ValueTypeOneofCase.StringValue:
                    parsedValue = confValue.StringValue;
                    break;
                case Value.ValueTypeOneofCase.TimestampValue:
                    parsedValue = confValue.TimestampValue.ToDateTime();
                    break;
                case Value.ValueTypeOneofCase.BlobValue:
                    parsedValue = confValue.BlobValue.ToByteArray();
                    break;
                case Value.ValueTypeOneofCase.GeoPointValue:
                    parsedValue = ConfigurationValue.FromObject(new
                    {
                        SchemaType = "GeoPoint",
                        Latitude = confValue.GeoPointValue.Latitude, 
                        Longtitude = confValue.GeoPointValue.Longitude
                    });
                    break;
                case Value.ValueTypeOneofCase.ArrayValue:
                    parsedValue = ConfigurationValue.FromJSON(GenerateJSONFromValue(confValue));
                    break;
            }
            return parsedValue;
        }

        private JToken GenerateJSONFromValue(Value confValue)
        {
            switch(confValue.ValueTypeCase)
            {
                case Value.ValueTypeOneofCase.ArrayValue:
                    return new JArray(confValue.ArrayValue.Values.Select(GenerateJSONFromValue).ToArray());
                case Value.ValueTypeOneofCase.BlobValue:
                    return JValue.CreateString(confValue.BlobValue.ToBase64());
                case Value.ValueTypeOneofCase.BooleanValue:
                    return confValue.BooleanValue;
                case Value.ValueTypeOneofCase.DoubleValue:
                    return confValue.DoubleValue;
                case Value.ValueTypeOneofCase.IntegerValue:
                    return confValue.IntegerValue;
                case Value.ValueTypeOneofCase.NullValue:
                    return JValue.CreateNull();
                case Value.ValueTypeOneofCase.StringValue:
                    return JValue.CreateString(confValue.StringValue);
                case Value.ValueTypeOneofCase.TimestampValue:
                    return confValue.TimestampValue.ToDateTime();
                case Value.ValueTypeOneofCase.GeoPointValue:
                    var jGeoPoint = new JObject();
                    jGeoPoint["Latitude"] = confValue.GeoPointValue.Latitude;
                    jGeoPoint["Logitude"] = confValue.GeoPointValue.Longitude;
                    jGeoPoint["SchemaType"] = "GeoPont";
                    return jGeoPoint;
                case Value.ValueTypeOneofCase.None:
                    return JValue.CreateUndefined();
                case Value.ValueTypeOneofCase.EntityValue:
                    var jObject = new JObject();
                    jObject["$Key"] = confValue.EntityValue.Key != null ? confValue.EntityValue.Key.ToString() : null;
                    foreach(var prop in confValue.EntityValue.Properties)
                    {
                        jObject[prop.Key] = GenerateJSONFromValue(prop.Value.EntityValue.Properties[prop.Key]);
                    }
                    return jObject;
            }
            return JValue.CreateUndefined();
        }

        public async Task LoadValues(IDictionary<string, ConfigurationValue> values, bool forClient = false)
        {
            var datastoreClient = DatastoreDb.Create(this.ConfigurationNamespace);
            var query = new Query("ConfigurationValue");
            query.Filter = Filter.Property("visibleToClient", forClient, PropertyFilter.Types.Operator.Equal);
            var settingsResult = await datastoreClient.RunQueryAsync(query);
            foreach(var entry in settingsResult.Entities)
            {
                var confName = entry.Properties["Name"].StringValue;
                var confValue = entry.Properties["Value"];
                var parsedValue = ParseLodedConfigurationValue(confValue);
                values[confName] = parsedValue;
            }
        }
    }
}
