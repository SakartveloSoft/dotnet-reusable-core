using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class ScalarValue<TValue> : ComparationOperand<TValue> where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public readonly TValue Value;

        public override IReadOnlyList<FilterNode> Children => null;

        public ScalarValue(TValue val)
        {
            Value = val;
            NodeType = FilterNodeType.Value;
        }

        public static implicit operator ScalarValue<TValue>(TValue val)
        {
            return new ScalarValue<TValue>(val);
        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
        }
    }
}
