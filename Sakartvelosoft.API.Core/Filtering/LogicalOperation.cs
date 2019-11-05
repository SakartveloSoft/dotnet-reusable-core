using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public abstract class LogicalOperation : ComparationOperand<bool>
    {
        public static LogicalOperation operator &(LogicalOperation a, LogicalOperation b) 
        {
            return new BinaryLogicalOperation(a, b, LogicalOperator.And);
        }

        public static LogicalOperation operator |(LogicalOperation a, LogicalOperation b)
        {
            return new BinaryLogicalOperation(a, b, LogicalOperator.Or);
        }
        public static LogicalOperation operator !(LogicalOperation a)
        {
            return new UnaryLogicalOperation(a, LogicalOperator.Not);
        }

        public static bool operator true(LogicalOperation op)
        {
            //to make sure right part always computed for && and || 
            return true;
        }

        public static bool operator false(LogicalOperation op)
        {
            //to make sure right part always computed for && and || 
            return false;
        }

    }
}
