using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class MultOperandsBooleanOperation: LogicalOperation
    {
        public LogicalOperator Operation { get; private set; }

        private List<ComparationOperand<bool>> operands = new List<ComparationOperand<bool>>();

        public override IReadOnlyList<FilterNode> Children => operands;

        public MultOperandsBooleanOperation(LogicalOperator op, IEnumerable<ComparationOperand<bool>> args)
        {
            this.Operation = op;
            this.operands = args.ToList();
            this.NodeType = FilterNodeType.DynamicLogicOp;
        }

        public MultOperandsBooleanOperation(LogicalOperator op, params ComparationOperand<bool>[] args)
        {
            this.Operation = op;
            this.operands = args.ToList();
        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            foreach(var operand in operands)
            {
                operand.DetectNewParameters(parametersBag);
            }
        }
    }
}
