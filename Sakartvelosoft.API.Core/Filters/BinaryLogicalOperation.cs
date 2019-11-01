using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class BinaryLogicalOperation: LogicalOperation
    {
        public readonly ComparationOperand<bool> Left;
        public readonly ComparationOperand<bool> Right;
        public readonly LogicalOperator Operation;
        public BinaryLogicalOperation(ComparationOperand<bool> left, ComparationOperand<bool> right, LogicalOperator op)
        {
            Left = left;
            Right = right;
            Operation = op;
            NodeType = FilterNodeType.BinaryLogicOp;

        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            Left.DetectNewParameters(parametersBag);
        }
    }
}
