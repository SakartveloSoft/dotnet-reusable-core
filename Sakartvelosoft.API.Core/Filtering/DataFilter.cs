using SakartveloSoft.API.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class DataFilter<T> where T: class, new()
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

        public bool HasParameter(string name)
        {
            return this.KnownParameters.ContainsKey(name);
        }

        public bool HasParameter<TValue>(string name)
        {
            return this.KnownParameters.ContainsKey(name) && KnownParameters[name].ValueType == typeof(TValue);
        }

        public bool ParameterHasValue(string name)
        {
            return this.Parameters.ContainsKey(name);
        }

        public object GetParameterValue(string name)
        {
            return this.Parameters[name];
        }
        public TValue GetParameter<TValue>(string name, TValue fallbackValue = default)
        {
            if (!KnownParameters.ContainsKey(name))
            {
                throw new ArgumentException(name, "Unknown parameter");
            }
            var paramRef = KnownParameters[name];
            if (Parameters.TryGetValue(name, out object val)) { 
                if (val == null)
                {
                    return fallbackValue;
                }
                return (TValue)Convert.ChangeType(this.Parameters[paramRef.Name], typeof(TValue));
            } 
            else
            {
                return fallbackValue;
            }

        }

        public TValue GetParameter<TValue>(FilterParameter<TValue> parameterRef) where TValue : IComparable<TValue>, IEquatable<TValue> 
        {
            return GetParameter<TValue>(parameterRef.Name);
        }


        public static implicit operator DataFilter<T>(LogicalOperation compare)
        {
            return new DataFilter<T>(compare);
        }
    }
}
