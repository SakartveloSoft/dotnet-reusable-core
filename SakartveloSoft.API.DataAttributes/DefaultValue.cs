using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.DataAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class DefaultValue: Attribute
    {
        public object Value
        {
            get {
                if (NumberValue.HasValue)
                {
                    return NumberValue.Value;
                }
                if (BooleanValue.HasValue)
                {
                    return BooleanValue.Value;
                }
                return (object)(NumberValue.HasValue ? (object)NumberValue.Value
                  : (BooleanValue.HasValue ? (object)BooleanValue.Value
                  : (DateTime.HasValue ? (object)DateTime.Value
                  : (DateTimeOffset.HasValue ? (object)DateTimeOffset.Value
                  : ((string)StringValue)
                  )
                  )
                  )
                  ); 
            }
        }
        public DefaultValueType ValueType { get; private set; }
        public decimal? NumberValue { get; private set; }
        public bool? BooleanValue { get; private set; }
        public string StringValue { get; private set; }
        public DateTime? DateTime { get; private set; }
        public DateTimeOffset? DateTimeOffset { get; private set; }
        public DefaultValue(int val)
        {
            ValueType = DefaultValueType.Int;
            NumberValue = val;
        }
        public DefaultValue(long val)
        {
            ValueType = DefaultValueType.Long;
            NumberValue = val;
        }
        public DefaultValue(float val)
        {
            ValueType = DefaultValueType.Float;
            NumberValue = (decimal)val;
        }
        public DefaultValue(double val)
        {
            ValueType = DefaultValueType.Double;
            NumberValue = (decimal)val;
        }
        public DefaultValue(decimal val)
        {
            ValueType = DefaultValueType.Decimal;
            NumberValue = val;
        }
        public DefaultValue(bool val)
        {
            ValueType = DefaultValueType.Boolean;
           BooleanValue = val;
        }
        public DefaultValue(string val)
        {
            ValueType = DefaultValueType.String;
            StringValue = val;
        }

        DefaultValue(DateTime val)
        {
            ValueType = DefaultValueType.DateTime;
            DateTime = val;
        }

        DefaultValue(DateTimeOffset val)
        {
            ValueType = DefaultValueType.DateTimeOffset;
            DateTimeOffset = val;
        }


    }
}
