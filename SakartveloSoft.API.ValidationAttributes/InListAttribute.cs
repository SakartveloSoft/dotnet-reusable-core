using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SakartveloSoft.API.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public sealed class InListAttribute : Attribute
    {
        public IReadOnlyList<object> Values { get; private set; }
        public InListAttribute(IEnumerable<object> values)
        {
            Values = values.ToList();
        }

        public InListAttribute(params object[] values)
        {
            Values = values;
        }
    }
}
