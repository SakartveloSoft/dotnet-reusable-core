using System.Collections.Generic;

namespace Sakartvelosoft.API.Core.Filters
{
    public class RawPropertyReference: DynamicOperand
    {
        public string Name { get; private set; }

        public RawPropertyReference(string name)
        {
            NodeType = FilterNodeType.Property;
            this.Name = name;
        }

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
            
        }
    }
}