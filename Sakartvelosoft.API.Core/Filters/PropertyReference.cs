using Sakartvelosoft.API.Core.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sakartvelosoft.API.Core.Filters
{
    public class PropertyReference<T, TKey, TProperty> : ComparationOperand<TProperty>
        where T: IEntityWithKey<TKey>
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

        public override void DetectNewParameters(IDictionary<string, IParameterReference> parametersBag)
        {
        }
    }
}
