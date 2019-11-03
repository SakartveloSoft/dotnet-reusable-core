using Sakartvelosoft.API.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.Json;

namespace Sakartvelosoft.API.Core.Filters
{
    public class FltersBuilder<T> where T : class, new()
    {
        public ComparationOperand<TProperty> Property<TProperty>(params string[] names)
            where TProperty : IEquatable<TProperty>, IComparable<TProperty>
        {
            return new PropertyReference<T, TProperty>(names);
        }

        public ComparationOperand<TProperty> Property<TProperty>(Expression<Func<T, TProperty>> lambda)
            where TProperty : IEquatable<TProperty>, IComparable<TProperty>
        {
            return new PropertyReference<T, TProperty>(Filters.GetLambdaDataPath(lambda.Body));
        }


        public DataFilter<T> Compare<TProperty>(string name, TProperty value, FilterComparison op = FilterComparison.Equal)
            where TProperty : IComparable<TProperty>, IEquatable<TProperty>
        {
            return new DataFilter<T>(new CompareOperation<TProperty>(new PropertyReference<T, TProperty>(name), new ScalarValue<TProperty>(value), op));
        }

        public FilterParameter<TValue> Parameter<TValue>(string name) where TValue : IComparable<TValue>, IEquatable<TValue>
        {
            return new FilterParameter<TValue>(name);
        }

        public DataFilter<T> FromJSON(JsonElement value) 
        {
            return new DataFilter<T>(ParseFilterCondition(value));
        }

        private LogicalOperation ParseFilterCondition(JsonElement value, string name = null)
        {
            JsonElement elem;
            if (value.TryGetProperty("$or", out elem)) {
                return BuildConditionsMerger(elem, LogicalOperator.Or);
            }
            if (value.TryGetProperty("$and", out elem))
            {
                return BuildConditionsMerger(elem, LogicalOperator.And);
            }
            if (value.TryGetProperty("$eq", out elem))
            {
                return new DynamicComparation(name, GetValueFromElem(elem), FilterComparison.Equal);
            }
            if (value.TryGetProperty("$neq", out elem))
            {
                return new DynamicComparation(name, GetValueFromElem(elem), FilterComparison.NotEqual);
            }
            if (value.TryGetProperty("$gt", out elem))
            {
                return new DynamicComparation(name, GetValueFromElem(elem), FilterComparison.Greater);
            }
            if (value.TryGetProperty("$gte", out elem))
            {
                return new DynamicComparation(name, GetValueFromElem(elem), FilterComparison.GreaterOrEqual);
            }
            if (value.TryGetProperty("$lt", out elem))
            {
                return new DynamicComparation(name, GetValueFromElem(elem), FilterComparison.Less);
            }
            if (value.TryGetProperty("$lte", out elem))
            {
                return new DynamicComparation(name, GetValueFromElem(elem), FilterComparison.LessOrEqual);
            }

            var comparations = new List<LogicalOperation>();
            foreach (var prop in value.EnumerateObject())
            {
                if (prop.Name.StartsWith("$"))
                {
                    throw new NotImplementedException($@"Operation not implemented: {prop.Name}");
                }
                if (prop.Value.ValueKind == JsonValueKind.Object)
                {
                    comparations.Add(ParseFilterCondition(prop.Value, prop.Name));
                }
                else
                {
                    comparations.Add(CompareValue(prop.Name, GetValueFromElem(prop.Value)));
                }
            }
            return new MultOperandsBooleanOperation(LogicalOperator.And, comparations);
        }

        private RawPropertyReference RawProperty(string name)
        {
            return new RawPropertyReference(name);
        }

        public DynamicScalar DynamicValue(object value)
        {
            return new DynamicScalar(value);
        }


        public static LogicalOperation CompareValue(string propName, object value, FilterComparison op = FilterComparison.Equal)
        {
            return new DynamicComparation(propName, value, op);
        }


        private object GetValueFromElem(JsonElement elem)
        {
            switch(elem.ValueKind)
            {
                case JsonValueKind.Array:
                    var arr = new List<object>();
                    foreach(var value in elem.EnumerateArray())
                    {
                        arr.Add(GetValueFromElem(value));
                    }
                    return arr;
                case JsonValueKind.Object:
                    var map = new Dictionary<string, object>();
                    foreach(var prop in elem.EnumerateObject())
                    {
                        map.Add(prop.Name, GetValueFromElem(prop.Value));
                    }
                    return map;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.Null:
                case JsonValueKind.Undefined:
                    return null;
                case JsonValueKind.Number:
                    if (elem.TryGetInt32(out int i))
                    {
                        return i;
                    }
                    if (elem.TryGetInt64(out long l))
                    {
                        return l;
                    }
                    if (elem.TryGetDouble(out double d))
                    {
                        return d;
                    }
                    throw new ArgumentException("Invalid number " + elem.GetRawText());
                case JsonValueKind.String:
                    if (elem.TryGetDateTime(out DateTime dt)) {
                        return dt;
                    }
                    if (elem.TryGetDateTimeOffset(out DateTimeOffset dto))
                    {
                        return dto;
                    }
                    if (elem.TryGetGuid(out Guid uid))
                    {
                        return uid;
                    }
                    return elem.GetRawText();
            }
            throw new NotImplementedException();
        }

        private LogicalOperation BuildConditionsMerger(JsonElement elem, LogicalOperator op)
        {
            var conditions = new List<ComparationOperand<bool>>();
            if (elem.ValueKind == JsonValueKind.Array) {
                foreach (var subElem in elem.EnumerateArray()) {
                    conditions.Add(ParseFilterCondition(subElem));
                }
            } else if (elem.ValueKind == JsonValueKind.Object)
            {
                foreach(var prop in elem.EnumerateObject())
                {
                    conditions.Add(ParseFilterCondition(prop.Value, prop.Name));
                }
            }
            
            return new MultOperandsBooleanOperation(op, conditions);
        }

        public DataFilter<T> FromJSON(string jsonDocText)
        {
            using (var doc = JsonDocument.Parse(jsonDocText))
            {
                return new DataFilter<T>(ParseFilterCondition(doc.RootElement));
            }
        }

        public DataFilter<T> Build(LogicalOperation op, object parametersBag = null)
        {
            return new DataFilter<T>(operation: op, Filters.ParseValuesBag(parametersBag));
        }
    }
}