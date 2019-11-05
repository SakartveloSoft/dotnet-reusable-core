using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public abstract class FilterNode
    {
        public FilterNodeType NodeType { get; protected set; }

        public abstract void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag);

        public abstract IReadOnlyList<FilterNode> Children { get; }
    }
}
