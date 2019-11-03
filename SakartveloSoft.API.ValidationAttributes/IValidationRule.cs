using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    public interface IValidationRule<T>
    {
        bool IsValueValid(T value);
    }
}
