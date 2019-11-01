using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public abstract class FilterNode
    {
        public FilterNodeType NodeType { get; protected set; }

        public abstract void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag);
    }
}
