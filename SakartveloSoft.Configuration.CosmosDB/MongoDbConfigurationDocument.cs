using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.Configuration.CosmosDB
{
    public class MongoDbConfigurationDocument
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("component")]
        public string Component { get; set; }
        [BsonElement("path")]
        public string Path { get; set; }
        [BsonElement("valueType")]
        public string ValueType { get; set; }
        [BsonElement("stringValue"), BsonIgnoreIfNull]
        public string StringValue { get; set; }
        [BsonElement("longValue"), BsonIgnoreIfNull]
        public long? LongValue { get; set; }
        [BsonElement("doubleValue"), BsonIgnoreIfNull]
        public double? DoubleValue { get; set; }
        [BsonElement("booleanValue"), BsonIgnoreIfNull]
        public bool? BooleanValue { get; set; }
        [BsonElement("bytesValue"), BsonIgnoreIfNull]
        public byte[] BytesValue { get; set; }
        [BsonElement("meaning"), BsonIgnoreIfNull]
        public string Meaning { get; set; }
        [BsonElement("visibleToPages")]
        public bool VisibleToPages { get; internal set; }
        [BsonElement("label"), BsonIgnoreIfNull]
        public string Label { get; set; }
    }
}
