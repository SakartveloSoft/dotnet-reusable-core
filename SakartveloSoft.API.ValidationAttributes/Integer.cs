using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class Integer : ValidationAttribute, IScalarValidationRule<int>, IValidationRule<int>, IValidationRule<long>, IScalarValidationRule<long>, IScalarValidationRule<decimal>
    {
        public Integer(string message = null):base(message ?? "{name} must be a valid integer")
        {

        }
        public override bool CanValidateType(Type type)
        {
            return type == typeof(int) || type == typeof(long) || type == typeof(decimal)
                || type == typeof(int?) || type == typeof(long?) || type == typeof(decimal?);
        }

        public bool IsValid(int? value)
        {
            return value.HasValue;
        }

        public bool IsValid(long? value)
        {
            return value.HasValue;
        }

        public bool IsValid(decimal? value)
        {
            return value.HasValue && Math.Floor(value.Value) == value.Value;
        }

        public override bool IsValueValid(object value)
        {
            if (value != null)
            {
                var valueType = value.GetType();
                if (valueType == typeof(decimal))
                {
                    var val = (decimal)value;
                    if (Math.Floor(val) != val)
                    {
                        return false;
                    }
                }
                if (valueType == typeof(int) || valueType == typeof(long))
                {
                    return true;
                }
                if (valueType == typeof(int?))
                {
                    return ((int?)value).HasValue;
                }
                if (valueType == typeof(long?))
                {
                    return ((long?)value).HasValue;
                }
                if (valueType == typeof(decimal?))
                {
                    var dec = (decimal?)value;
                    return dec.HasValue && Math.Floor(dec.Value) == dec.Value;
                }
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool IsValueValid(int value)
        {
            return true;
        }

        public bool IsValueValid(long value)
        {
            return true;
        }

        public bool IsValueValid(decimal value)
        {
            return Math.Floor(value) == value;
        }
    }
}
