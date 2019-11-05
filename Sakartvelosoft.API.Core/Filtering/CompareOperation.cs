using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class CompareOperation<TValue>: LogicalOperation where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        public readonly ComparationOperand<TValue> Left;
        public readonly ComparationOperand<TValue> Right;

        public readonly FilterComparison Operation;

        private readonly List<FilterNode> operands = new List<FilterNode>();

        public override IReadOnlyList<FilterNode> Children => operands;

        public CompareOperation(ComparationOperand<TValue> left, ComparationOperand<TValue> right, FilterComparison op = FilterComparison.Equal)
        {
            Left = left;
            Right = right;
            Operation = op;
            NodeType = FilterNodeType.Compare;
            operands.Add(left);
            operands.Add(right);

        }


        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            Left.DetectNewParameters(parametersBag);
            Right.DetectNewParameters(parametersBag);
        }
    }
}
