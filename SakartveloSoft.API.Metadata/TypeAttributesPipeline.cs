using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SakartveloSoft.API.Metadata
{
    public class TypeAttributesPipeline
    {
        private List<Action<MetaType, IEnumerable<Attribute>>> handlers = new List<Action<MetaType, IEnumerable<Attribute>>>();

        public void AddAttributeHandler<TAtribute>(Action<MetaType, TAtribute> handler) where TAtribute: Attribute
        {
            var attributeClass = typeof(TAtribute);
            handlers.Add((MetaType metaType, IEnumerable<Attribute> attributes) =>
            {
                foreach(var attr in attributes.OfType<TAtribute>())
                {
                    handler(metaType, attr);
                }
            });
        }

        public void ApplyDiscoveredAttributes(MetaType metaType, IEnumerable<Attribute> attributes)
        {
            foreach(var act in handlers)
            {
                act(metaType, attributes);
            }
        }
    }
}
