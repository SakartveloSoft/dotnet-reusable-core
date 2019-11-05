using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public interface IParameterReference
    {
        string Name { get; }

        public Type ValueType { get; }
    }
}
