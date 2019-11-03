using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class FloatingNumber : ValidationAttribute, IScalarValidationRule<float>, IScalarValidationRule<double>, IScalarValidationRule<decimal>
    {
        public FloatingNumber(string message = null) : base(message ?? "{name} must be a valid floating number")
        {

        }

        public override bool CanValidateType(Type type)
        {
            return type == typeof(float) || type == typeof(double) || type == typeof(decimal)
                || type == typeof(float?) || type == typeof(double?) || type == typeof(decimal?);
        }

        public bool IsValid(float? value)
        {
            return value.HasValue && float.IsFinite(value.Value);
        }

        public bool IsValid(double? value)
        {
            return value.HasValue && !double.IsFinite(value.Value);
        }

        public bool IsValid(decimal? value)
        {
            return value.HasValue;
        }

        public bool IsValueValid(float value)
        {
            return float.IsFinite(value);
        }

        public bool IsValueValid(double value)
        {
            return double.IsFinite(value);
        }

        public bool IsValueValid(decimal value)
        {
            return true;
        }

        public override bool IsValueValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            var valueType = value.GetType();
            if(valueType == typeof(double))
            {
                return IsValueValid((double)value);
            }
            if (valueType == typeof(float))
            {
                return IsValueValid((float)value);
            }
            if (valueType == typeof(decimal))
            {
                return IsValueValid((decimal)value);
            }
            return false;
        }
    }
}
