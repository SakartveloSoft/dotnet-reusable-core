using Sakartvelosoft.API.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Sakartvelosoft.API.Core.Filters
{
    public class FltersBuilder<T, TKey> where T : class, DataModel.IEntityWithKey<TKey>, new()
    {
        public ComparationOperand<TProperty> Property<TProperty>(params string[] names)
            where TProperty : IEquatable<TProperty>, IComparable<TProperty>
        {
            return new PropertyReference<T, TKey, TProperty>(names);
        }

        public ComparationOperand<TProperty> Property<TProperty>(Expression<Func<T, TProperty>> lambda)
            where TProperty : IEquatable<TProperty>, IComparable<TProperty>
        {
            return new PropertyReference<T, TKey, TProperty>(Filters.GetLambdaDataPath(lambda.Body));
        }


        public DataFilter<T, TKey> Compare<TProperty>(string name, TProperty value, FilterComparison op = FilterComparison.Equal)
            where TProperty : IComparable<TProperty>, IEquatable<TProperty>
        {
            return new DataFilter<T, TKey>(new CompareOperation<TProperty>(new PropertyReference<T, TKey, TProperty>(name), new ScalarValue<TProperty>(value), op));
        }

        public ComparationOperand<TValue> Parameter<TValue>(string name) where TValue : IComparable<TValue>, IEquatable<TValue>
        {
            return new FilterParameter<TValue>(name);
        }

        public DataFilter<T, TKey> Build(LogicalOperation op, object parametersBag)
        {
            return new DataFilter<T, TKey>(operation: op, Filters.ParseValuesBag(parametersBag));
        }
    }
}