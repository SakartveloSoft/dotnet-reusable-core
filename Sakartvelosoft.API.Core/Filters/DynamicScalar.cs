using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class DynamicScalar : DynamicOperand
    {
        public object Value { get; private set; }

        public bool IsNull { get; private set; }
        public DynamicScalar(object value)
        {
            NodeType = FilterNodeType.Value;
            Value = value;
        }
        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
        }
    }
}
