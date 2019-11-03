using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    interface IScalarValidationRule<T>: IValidationRule<T> where T: struct
    {
        bool IsValid(T? value);
    }
}
