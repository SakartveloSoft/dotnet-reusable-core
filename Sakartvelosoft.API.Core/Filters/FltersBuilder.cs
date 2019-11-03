using Sakartvelosoft.API.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

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

        public DataFilter<T> Build(LogicalOperation op, object parametersBag = null)
        {
            return new DataFilter<T>(operation: op, Filters.ParseValuesBag(parametersBag));
        }
    }
}