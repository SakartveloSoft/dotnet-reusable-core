using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public enum FilterNodeType
    {
        Unknown,
        Compare,
        BinaryLogicOp,
        UnaryLogicOp,
        Value,
        Property,
        Parameter,
        ValueInList
    }
}
