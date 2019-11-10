using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text.Json;

namespace SakartveloSoft.API.Core.Configuration
{
    public class ConfigurationValue
    {
        public ConfigurationValueType Type { get; private set; }
        public Type ValueType { get; private set; }
        public string StringValue { get; private set; }
        public int IntegerValue { get; private set; }
        public double DoubleValue { get; private set; }
        public bool BooleanValue { get; private set; }
        public DateTime DateTime { get; private set; }
        public TimeSpan TimeSpan { get; private set; }
        public object Value { get; private set; }
        public dynamic DynamicValue { get; private set; }
        public byte[] BytesValue { get; private set; }

        public static readonly ConfigurationValue NullValue = new ConfigurationValue
        {
            Type = ConfigurationValueType.Null
        };

        public static implicit operator ConfigurationValue(string value)
        {
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.String,
                StringValue = value,
                Value = value,
                DynamicValue = value,
                ValueType = typeof(String)
            };
        }
        public static implicit operator ConfigurationValue(int value)
        {
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.Integer,
                StringValue = value.ToString(),
                IntegerValue = value,
                DoubleValue = value,
                Value = value,
                DynamicValue = value,
                ValueType = typeof(int)
            };
        }
        public static implicit operator ConfigurationValue(double value)
        {
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.Double,
                StringValue = value.ToString(),
                DoubleValue = value,
                Value = value,
                DynamicValue = value,
                ValueType = typeof(double)
            };
        }
        public static implicit operator ConfigurationValue(bool value)
        {
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.Boolean,
                StringValue = value.ToString(),
                BooleanValue = value,
                IntegerValue = value ? 1 : 0,
                DoubleValue = value ? 1 : 0,
                Value = value,
                DynamicValue = value,
                ValueType = typeof(bool)
            };
        }

        public static implicit operator ConfigurationValue(DateTime value)
        {
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.DateTime,
                DateTime = value,
                BooleanValue = value.Ticks != 0,
                DoubleValue = value.Ticks,
                Value = value,
                DynamicValue = value,
                StringValue = value.ToString("yyyy-MM-ddThh:mm:ss.fffZ"),
            };
        }

        public static implicit operator ConfigurationValue(byte[] bytes)
        {
            if (bytes == null)
            {
                return NullValue;
            }
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.Bytes,
                StringValue = Convert.ToBase64String(bytes),
                Value = bytes,
                DynamicValue = bytes,
                BytesValue = bytes,
            };
        }

        public static ConfigurationValue FromObject<T>(T value) where T : class
        {
            var jsonValue = JsonSerializer.Serialize<T>(value);
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.Object,
                Value = value,
                StringValue = jsonValue,
                DynamicValue = value
            };
        }

        private static dynamic ParseNode(JsonElement jsonElement)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.Null:
                    return null;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.String:
                    return jsonElement.GetString();
                case JsonValueKind.Number:
                    if (jsonElement.TryGetInt32(out int i32))
                    {
                        return i32;
                    }
                    if (jsonElement.TryGetDouble(out double doubleVal))
                    {
                        return doubleVal;
                    }
                    throw new Exception($@"Can not parse element ${jsonElement.GetRawText()} as a number");
                case JsonValueKind.Array:
                    var values = new List<dynamic>(jsonElement.GetArrayLength());
                    for (var x = 0; x < values.Capacity; x++)
                    {
                        values[x] = ParseNode(jsonElement[x]);
                    }
                    return values;
                case JsonValueKind.Object:
                    dynamic expando = new ExpandoObject();
                    var dict = (IDictionary<string, object>)expando;
                    foreach (var prop in jsonElement.EnumerateObject())
                    {
                        dict[prop.Name] =  ParseNode(prop.Value);
                    }
                    return expando;
                case JsonValueKind.Undefined:
                default:
                    return null;
            }
        }

        private static dynamic MakeDynamicFromJSON(string json)
        {
            using(JsonDocument doc = JsonDocument.Parse(json))
            {
                return ParseNode(doc.RootElement);
            }

        }

        public static ConfigurationValue FromJSON(JToken token)
        {
            var jsDoc = MakeDynamicFromJSON(token.ToString());
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.Json,
                StringValue = token.ToString(),
                Value = jsDoc,
                DynamicValue = jsDoc
            };
        }

        public static ConfigurationValue FromJSON(string jsonLiteral)
        {
            dynamic jsDoc = MakeDynamicFromJSON(jsonLiteral);
            return new ConfigurationValue
            {
                Type = ConfigurationValueType.Object,
                StringValue = jsonLiteral,
                DynamicValue = jsDoc,
                Value = jsDoc
            };
        }
    }
}