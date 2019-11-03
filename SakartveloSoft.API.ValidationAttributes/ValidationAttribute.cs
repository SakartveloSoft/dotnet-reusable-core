using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    public abstract class ValidationAttribute : Attribute, IValuesValidator
    {
        public string MessageTemplate { get; protected set; }

        protected ValidationAttribute(string messsage = null)
        {
            MessageTemplate = messsage ?? "Attribute {name} has invalid value";
        }

        public abstract bool IsValueValid(object value);

        public abstract bool CanValidateType(Type type);

        public abstract string ErrorCode { get; }
    }
}
