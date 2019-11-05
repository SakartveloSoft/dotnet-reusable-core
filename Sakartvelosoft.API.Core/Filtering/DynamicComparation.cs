using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class DynamicComparation: LogicalOperation
    {
        public string PropertyName { get; private set; }

        public object Value { get; private set; }

        protected List<FilterNode> operands = new List<FilterNode>(2);

        public FilterComparison Operation { get; private set; }

        public override IReadOnlyList<FilterNode> Children => operands;

        public DynamicComparation(string property, object value, FilterComparison op = FilterComparison.Equal)
        {
            PropertyName = property;
            Value = value;
            Operation = op;
            NodeType = FilterNodeType.Compare;
            operands.Add(new RawPropertyReference(PropertyName));
            operands.Add(new DynamicScalar(value));

        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
        }
    }
}
