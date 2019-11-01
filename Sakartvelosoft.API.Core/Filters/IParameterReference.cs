using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public interface IParameterReference
    {
        string Name { get; }

        public Type ValueType { get; }
    }
}
