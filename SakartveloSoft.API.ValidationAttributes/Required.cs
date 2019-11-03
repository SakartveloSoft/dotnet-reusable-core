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

        public override bool CanValidateType(Type type)
        {
            return true;
        }

        public override bool IsValueValid(object value)
        {
            return value != null;
        }
    }

}
