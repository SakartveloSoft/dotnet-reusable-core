using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class UnaryLogicalOperation : LogicalOperation
    {
        public readonly ComparationOperand<bool> Operand;
        public readonly LogicalOperator Operator;
        private List<FilterNode> operands = new List<FilterNode>(1);
        public UnaryLogicalOperation(ComparationOperand<bool> operand, LogicalOperator op)
        {
            Operand = operand;
            Operator = op;
            NodeType = FilterNodeType.UnaryLogicOp;
            operands.Add(operand);
        }

        public override IReadOnlyList<FilterNode> Children => operands;

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            Operand.DetectNewParameters(parametersBag);
        }
    }
}
