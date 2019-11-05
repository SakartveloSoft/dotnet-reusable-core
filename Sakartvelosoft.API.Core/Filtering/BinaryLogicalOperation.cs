using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class BinaryLogicalOperation: LogicalOperation
    {
        public readonly ComparationOperand<bool> Left;
        public readonly ComparationOperand<bool> Right;
        public readonly LogicalOperator Operation;
        private List<ComparationOperand<bool>> operands = new List<ComparationOperand<bool>>(2);
        public BinaryLogicalOperation(ComparationOperand<bool> left, ComparationOperand<bool> right, LogicalOperator op)
        {
            Left = left;
            Right = right;
            Operation = op;
            NodeType = FilterNodeType.BinaryLogicOp;
            operands.Add(left);
            operands.Add(right);

        }

        public override IReadOnlyList<FilterNode> Children => operands;

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            Left.DetectNewParameters(parametersBag);
            Right.DetectNewParameters(parametersBag);
        }
    }
}
