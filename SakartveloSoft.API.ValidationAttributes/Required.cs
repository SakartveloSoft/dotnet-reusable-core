using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class Required : ValidationAttribute
    {
        public Required(string message = null):base(message ?? "{name} is required") {
        }

        public override string ErrorCode => ErrorCodes.Required;

        public override bool CanValidateType(Type type)
        {
            return true;
        }

        public override bool IsValueValid(object value)
        {
            if (value == null)
            {
                return false;
            }
            if (typeof(string).IsInstanceOfType(value))
            {
                return !value.Equals(String.Empty);
            }
            return true;
        }
    }

}
