using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class InListComparation<T, TValue>: LogicalOperation where T: class, new() where TValue: IEquatable<TValue>, IComparable<TValue>
    {
        public PropertyReference<T, TValue> Property { get; private set; }
        public IReadOnlyList<TValue> Values { get; private set; }
        public FilterComparison Operation { get; private set; }

        private List<FilterNode> operands = new List<FilterNode>();

        public override IReadOnlyList<FilterNode> Children => throw new NotImplementedException();

        public InListComparation(PropertyReference<T, TValue> property, IEnumerable<TValue> values)
        {
            Property = property;
            Values = values.ToList();
            operands.Add(property);
            operands.AddRange(Values.Select(val => new ScalarValue<TValue>(val)));
            NodeType = FilterNodeType.ValueInList;
            Operation = FilterComparison.ValueInList;
        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            //
        }
    }
}
