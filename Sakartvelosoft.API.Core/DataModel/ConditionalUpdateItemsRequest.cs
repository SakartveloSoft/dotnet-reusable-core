using Sakartvelosoft.API.Core.Filters;
using System.Collections.Generic;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class ConditionalUpdateItemsRequest<T, TKey> where T: class, IEntityWithKey<TKey>, new()
    {
        public DataFilter<T, TKey> Filter { get; set; }

        public List<EntityPropertyChange> Changes { get; set; }
    }
}