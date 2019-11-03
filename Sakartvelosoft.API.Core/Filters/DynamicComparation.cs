using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class DynamicComparation: LogicalOperation
    {
        public string PropertyName { get; private set; }

        public object Value { get; private set; }

        public FilterComparison Operation { get; private set; }

        public DynamicComparation(string property, object value, FilterComparison op = FilterComparison.Equal)
        {
            PropertyName = property;
            Value = value;
            Operation = op;
            NodeType = FilterNodeType.DynamicCompare;
        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
        }
    }
}
