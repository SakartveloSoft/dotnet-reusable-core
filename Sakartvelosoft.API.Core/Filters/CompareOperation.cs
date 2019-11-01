using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class CompareOperation<TValue>: LogicalOperation where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        public readonly ComparationOperand<TValue> Left;
        public readonly ComparationOperand<TValue> Right;

        public readonly FilterComparison Operation;

        public CompareOperation(ComparationOperand<TValue> left, ComparationOperand<TValue> right, FilterComparison op = FilterComparison.Equal)
        {
            Left = left;
            Right = right;
            Operation = op;
            NodeType = FilterNodeType.Compare;

        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            Left.DetectNewParameters(parametersBag);
            Right.DetectNewParameters(parametersBag);
        }
    }
}
