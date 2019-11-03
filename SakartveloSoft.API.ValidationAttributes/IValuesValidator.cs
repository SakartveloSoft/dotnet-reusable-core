using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    public interface IValuesValidator
    {
        public string MessageTemplate { get; }
        string ErrorCode { get; }

        bool IsValueValid(object value);

        bool CanValidateType(Type type);

    }
}
