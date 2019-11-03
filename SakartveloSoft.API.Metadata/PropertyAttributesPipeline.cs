using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SakartveloSoft.API.Metadata
{
    public class PropertyAttributesPipeline
    {
        private List<Action<MetaType, MetaProperty, IEnumerable<Attribute>>> handlers = new List<Action<MetaType, MetaProperty, IEnumerable<Attribute>>>();

        public void AddAttributeHandler<TAttribute>(Action<MetaType, MetaProperty, TAttribute> handler) where TAttribute: Attribute
        {
            var attributeType = typeof(TAttribute);
            handlers.Add((MetaType metaType, MetaProperty prop, IEnumerable<Attribute> attributes) =>
            {
                foreach(var attr in attributes.OfType<TAttribute>())
                {
                    handler(metaType, prop, attr);
                }
            });
        }

        public void ApplyDiscoveredAttributes(MetaType metaType, MetaProperty prop, IEnumerable<Attribute> attributes)
        {
            foreach (var act in handlers)
            {
                act(metaType, prop, attributes);
            }
        }
    }
}
