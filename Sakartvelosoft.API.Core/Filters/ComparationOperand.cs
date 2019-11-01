using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public abstract class ComparationOperand<TValue>: FilterNode where TValue: IEquatable<TValue>, IComparable<TValue>
    {
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static LogicalOperation operator ==(ComparationOperand<TValue> a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(a, b, FilterComparison.Equal);
        }

        public static LogicalOperation operator !=(ComparationOperand<TValue> a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(a, b, FilterComparison.NotEqual);
        }

        public static LogicalOperation operator >(ComparationOperand<TValue> a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(a, b, FilterComparison.Greater);
        }

        public static LogicalOperation operator >=(ComparationOperand<TValue> a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(a, b, FilterComparison.GreaterOrEqual);
        }

        public static LogicalOperation operator <(ComparationOperand<TValue> a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(a, b, FilterComparison.Less);
        }

        public static LogicalOperation operator <=(ComparationOperand<TValue> a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(a, b, FilterComparison.LessOrEqual);
        }


        public static LogicalOperation operator ==(ComparationOperand<TValue> a, TValue b)
        {
            return new CompareOperation<TValue>(a, new ScalarValue<TValue>(b), FilterComparison.Equal);
        }

        public static LogicalOperation operator !=(ComparationOperand<TValue> a, TValue b)
        {
            return new CompareOperation<TValue>(a, new ScalarValue<TValue>(b), FilterComparison.NotEqual);
        }

        public static LogicalOperation operator >(ComparationOperand<TValue> a, TValue b)
        {
            return new CompareOperation<TValue>(a, new ScalarValue<TValue>(b), FilterComparison.Greater);
        }

        public static LogicalOperation operator >=(ComparationOperand<TValue> a, TValue b)
        {
            return new CompareOperation<TValue>(a, new ScalarValue<TValue>(b), FilterComparison.GreaterOrEqual);
        }

        public static LogicalOperation operator <(ComparationOperand<TValue> a, TValue b)
        {
            return new CompareOperation<TValue>(a, new ScalarValue<TValue>(b), FilterComparison.Less);
        }
        public static LogicalOperation operator <=(ComparationOperand<TValue> a, TValue b)
        {
            return new CompareOperation<TValue>(a, new ScalarValue<TValue>(b), FilterComparison.LessOrEqual);
        }


        public static LogicalOperation operator ==(TValue a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(new ScalarValue<TValue>(a), b, FilterComparison.Equal);
        }

        public static LogicalOperation operator !=(TValue a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(new ScalarValue<TValue>(a), b, FilterComparison.NotEqual);
        }

        public static LogicalOperation operator >(TValue a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(new ScalarValue<TValue>(a), b, FilterComparison.Greater);
        }

        public static LogicalOperation operator >=(TValue a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(new ScalarValue<TValue>(a), b, FilterComparison.GreaterOrEqual);
        }

        public static LogicalOperation operator <(TValue a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(new ScalarValue<TValue>(a), b, FilterComparison.Less);
        }
        public static LogicalOperation operator <=(TValue a, ComparationOperand<TValue> b)
        {
            return new CompareOperation<TValue>(new ScalarValue<TValue>(a), b, FilterComparison.LessOrEqual);
        }


    }
}
