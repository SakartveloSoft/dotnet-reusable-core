using SakartveloSoft.API.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SakartveloSoft.API.Core.Filtering
{
    public class PropertyReference<T, TProperty> : ComparationOperand<TProperty>
        where T: class, new()
        where TProperty: IEquatable<TProperty>, IComparable<TProperty>
    {
        public readonly IReadOnlyList<string> DataPath;

        public PropertyReference(params string[] names)
        {
            DataPath = names;
            NodeType = FilterNodeType.Property;
        }
        
        public PropertyReference(IEnumerable<string> dataPath)
        {
            DataPath = dataPath.ToArray();
            NodeType = FilterNodeType.Property;
        }

        public PropertyReference(IReadOnlyList<string> names)
        {
            DataPath = names;
            NodeType = FilterNodeType.Property;
        }

        public override IReadOnlyList<FilterNode> Children => null;

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
        }
    }
}
