using System.Collections.Generic;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class EntityChangesPack<T, Tkey> where T : class, IEntityWithKey<Tkey>, new()
    {
        public Tkey Key { get; set; }
        public List<EntityPropertyChange> Updates { get; set; }
    }
}