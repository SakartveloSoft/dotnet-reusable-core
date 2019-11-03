using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SakartveloSoft.API.DataAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IdPrefix : Attribute
    {
        public IdPrefix([NotNull] string prefix)
        {
            Prefix = prefix;
        }

        public string Prefix { get; }
    }
}
