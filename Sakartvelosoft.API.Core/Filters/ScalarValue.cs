using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class ScalarValue<TValue> : ComparationOperand<TValue> where TValue : IEquatable<TValue>, IComparable<TValue>
    {
        public readonly TValue Value;
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
