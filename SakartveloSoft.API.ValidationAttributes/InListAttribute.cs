using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public sealed class InListAttribute : Attribute, IValuesValidator
    {
        public IReadOnlyList<object> Values { get; private set; }

        public string MessageTemplate
        {
            get
            {
                return "Property {name} is not one of following values" + String.Join(", ", Values.ToArray());
            }
        }

        public string ErrorCode => ErrorCodes.OutOfListValue;

        public InListAttribute(IEnumerable<object> values)
        {
            Values = values.ToList();
        }

        public InListAttribute(params object[] values)
        {
            Values = values;
        }

        public bool IsValueValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            foreach(var val in Values)
            {
                if (val != null && (val == value || val.Equals(value)))
                {
                    return true;
                } 
            }
            return false;
        }

        public bool CanValidateType(Type type)
        {
            foreach(var val in Values)
            {
                if (val != null && val.GetType() == type)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
