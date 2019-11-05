using System.Collections.Generic;

namespace SakartveloSoft.API.Core.Filtering
{
    public class RawPropertyReference: DynamicOperand
    {
        public string Name { get; private set; }

        public override IReadOnlyList<FilterNode> Children => null;

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