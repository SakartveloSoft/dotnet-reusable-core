using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class InRangeAttribute: ValidationAttribute, IScalarValidationRule<int>, IScalarValidationRule<long>, IScalarValidationRule<decimal>, IScalarValidationRule<float>, IScalarValidationRule<double>
    {
        private static decimal? DecodeBoundary(double? value, string strValue)
        {
            return value.HasValue ? (decimal)value.Value : strValue != null ? decimal.Parse(strValue) : default(decimal?);
        }
        public InRangeAttribute(double min, string message= null): base(message ?? "{name} is out of expected range")
        {
            Min = DecodeBoundary(min, null);
            Max = default;
        }
        public InRangeAttribute(double min, double max, string message = null) : base(message ?? "{name} is out of expected range")
        {
            Min = DecodeBoundary(min, null);
            Max = DecodeBoundary(max, null);
        }
        public InRangeAttribute(string min, string message = null) : base(message ?? "{name} is out of expected range")
        {
            Min = DecodeBoundary(null, min);
            Max = default;
        }
        public InRangeAttribute(string min, string max, string message = null) : base(message ?? "{name} is out of expected range")
        {
            Min = DecodeBoundary(null, min);
            Max = DecodeBoundary(null, max); 
        }

        public decimal? Min { get;  set; }
        public decimal? Max { get;  set; }

        public override bool CanValidateType(Type type)
        {
            return type == typeof(int) || type == typeof(long) || type == typeof(float) || type == typeof(double) || type == typeof(decimal)
             || type == typeof(int?) || type == typeof(long?) || type == typeof(float?) || type == typeof(double?) || type == typeof(decimal?);
        }

        private bool CheckRange(decimal? value)
        {
            if (value.HasValue)
            {
                if (Min.HasValue && value.Value < Min.Value)
                {
                    return false;
                }
                if (Max.HasValue && value.Value > Max.Value)
                {
                    return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        public bool IsValid(int? value)
        {
            return CheckRange(value);
        }

        public bool IsValid(long? value)
        {
            return CheckRange(value);
        }

        public bool IsValid(decimal? value)
        {
            return CheckRange(value);
        }

        public override bool IsValueValid(object value)
        {
            if(value == null)
            {
                return true;
            }
            var valueType = value.GetType();
            if (valueType == typeof(int))
            {
                return IsValid((int)value);
            }
            if (valueType == typeof(long))
            {
                return IsValid((long)value);
            }
            if (valueType == typeof(float))
            {
                return IsValid((float)value);
            }
            if (valueType == typeof(float))
            {
                return IsValid((float)value);
            }
            return false;
        }

        public bool IsValueValid(int value)
        {
            throw new NotImplementedException();
        }

        public bool IsValueValid(long value)
        {
            return CheckRange(value);
        }

        public bool IsValueValid(decimal value)
        {
            return CheckRange(value);
        }

        public bool IsValid(float? value)
        {
            return CheckRange(value.HasValue ? new decimal(value.Value) : default(decimal?));
        }

        public bool IsValueValid(float value)
        {
            return CheckRange(new decimal(value));
        }

        public bool IsValid(double? value)
        {
            return CheckRange(value.HasValue ? new decimal(value.Value) : default(decimal?));
        }

        public bool IsValueValid(double value)
        {
            return CheckRange(new decimal(value));
        }
    }
}
