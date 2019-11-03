using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    public static class ErrorCodes
    {
        public static readonly string Required = "required";
        public static readonly string InvalidIntegerNumber = "invalid.number.integer";
        public static readonly string InvalidFloatingNumber = "invalid.number.floating";
        public static readonly string OutOfRangeValue = "invalid.range";
        public static readonly string OutOfListValue = "invald.list";
    }
}
