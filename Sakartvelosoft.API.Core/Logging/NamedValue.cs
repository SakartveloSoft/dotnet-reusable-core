using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public class NamedValue
    {
        public readonly string Name;
        public readonly string Value;

        public NamedValue(string name, object value)
        {
            Name = name;
            Value = value == null ? "null" : value.ToString();
        }

        public override string ToString()
        {
            return $@"{Name}={Value ?? "null"}";
        }
    }
}
