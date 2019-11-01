using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public enum FilterNodeType
    {
        Unknown,
        Compare,
        BinaryLogicOp,
        UnaryLogicOp,
        Value,
        Property,
        Parameter
    }
}
