using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SakartveloSoft.API.DataAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class StoreName : Attribute
    {
        public StoreName([NotNull]string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
