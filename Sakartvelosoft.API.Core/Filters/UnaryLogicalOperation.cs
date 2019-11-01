using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class UnaryLogicalOperation : LogicalOperation
    {
        public readonly ComparationOperand<bool> Operand;
        public readonly LogicalOperator Operator;
        public UnaryLogicalOperation(ComparationOperand<bool> operand, LogicalOperator op)
        {
            Operand = operand;
            Operator = op;
            NodeType = FilterNodeType.UnaryLogicOp;
        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            Operand.DetectNewParameters(parametersBag);
        }
    }
}
