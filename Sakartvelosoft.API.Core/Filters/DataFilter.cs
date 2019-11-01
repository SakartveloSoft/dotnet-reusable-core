using Sakartvelosoft.API.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class DataFilter<T, TKey> where T: class, IEntityWithKey<TKey>, new()
    {
        public readonly LogicalOperation Operation;

        public readonly IDictionary<string, object> Parameters = new Dictionary<string, object>();
        public readonly IDictionary<string, IParameterReference> KnownParameters;

        public DataFilter(LogicalOperation operation, IDictionary<string, object> paramsMap = null)
        {
            Operation = operation;
            Parameters = paramsMap ?? new Dictionary<string, object>();
            KnownParameters = new Dictionary<string, IParameterReference>();
            Operation.DetectNewParameters(KnownParameters);
        }


        public static implicit operator DataFilter<T, TKey>(LogicalOperation compare)
        {
            return new DataFilter<T, TKey>(compare);
        }
    }
}
