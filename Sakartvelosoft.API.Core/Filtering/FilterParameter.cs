using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class FilterParameter<TValue>: ComparationOperand<TValue>, IParameterReference where TValue : IComparable<TValue>, IEquatable<TValue>
    {
        public string Name { get; private set; }

        public Type ValueType => typeof(TValue);

        public override IReadOnlyList<FilterNode> Children => null;

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
