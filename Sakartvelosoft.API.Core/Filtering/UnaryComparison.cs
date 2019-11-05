using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class UnaryComparison : ComparationOperand<bool>
    {
        private List<FilterNode> operands = new List<FilterNode>(1);
        public override IReadOnlyList<FilterNode> Children => operands;

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            Operand.DetectNewParameters(parametersBag);
        }

        public LogicalOperator Operation { get; private set; }

        public ComparationOperand<bool> Operand { get; private set; }

        public UnaryComparison(ComparationOperand<bool> operand, LogicalOperator op)
        {
            Operand = operand;
            operands.Add(operand);
            Operation = op;

        }
    }
}
