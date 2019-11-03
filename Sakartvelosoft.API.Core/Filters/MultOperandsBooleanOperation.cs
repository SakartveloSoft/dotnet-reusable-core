using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class MultOperandsBooleanOperation: LogicalOperation
    {
        public LogicalOperator Operation { get; private set; }

        private List<ComparationOperand<bool>> operands = new List<ComparationOperand<bool>>();

        public IReadOnlyList<ComparationOperand<bool>> Operands { get { return operands; } }

        public MultOperandsBooleanOperation(LogicalOperator op, IEnumerable<ComparationOperand<bool>> args)
        {
            this.Operation = op;
            this.operands = args.ToList();
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
