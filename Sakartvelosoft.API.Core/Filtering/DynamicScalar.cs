using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class DynamicScalar : DynamicOperand
    {
        public object Value { get; private set; }

        public bool IsNull { get; private set; }

        public override IReadOnlyList<FilterNode> Children => null;

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
