using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class FilterParameter<TValue>: ComparationOperand<TValue>, IParameterReference where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        public string Name { get; private set; }

        public Type ValueType => typeof(TValue);

        public FilterParameter(string name)
        {
            Name = name;
            NodeType = FilterNodeType.Parameter;
        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            if (!parametersBag.ContainsKey(Name))
            {
                parametersBag.Add(Name, this);
            }
        }
    }
}
