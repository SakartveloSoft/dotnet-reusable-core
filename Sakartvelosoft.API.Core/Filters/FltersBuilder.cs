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

        public DataFilter<T> FromJSON<T>(JsonElement value) where T: class, new()
        {
            var filter = new DataFilter<T>(ParseFilterCondition(value));
        }

        private LogicalOperation ParseFilterCondition(JsonElement value, string name = null)
        {
            JsonElement elem;
            if (value.TryGetProperty("$or", out JsonElement elem)) {
                return BuildConditionsMerger(elem, LogicalOperator.Or);
            }
            if (value.TryGetProperty("$and", out elem))
            {

                return BuildConditionsMerger(elem, LogicalOperator.Or);
            }
            if (value.TryGetProperty("$eq", out elem))
            {
                return new ValueCompareOperation(RawProperty(name), GetValueFromElem(elem))
            }
            foreach (var prop in value.EnumerateObject())
            {

            }
        }

        private RawPropertyReference RawProperty(string name)
        {
            return new RawPropertyReference(name);
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
                    
            }
            throw new NotImplementedException();
        }

        private LogicalOperation BuildConditionsMerger(JsonElement elem, LogicalOperator or)
        {
            throw new NotImplementedException();
        }

        public DataFilter<T> FromJSON(string jsonDocText)
        {
            using (var doc = JsonDocument.Parse(jsonDocText))
            {
                return FromJSON(doc.RootElement);
            }
        }

        public DataFilter<T> Build(LogicalOperation op, object parametersBag = null)
        {
            return new DataFilter<T>(operation: op, Filters.ParseValuesBag(parametersBag));
        }
    }
}