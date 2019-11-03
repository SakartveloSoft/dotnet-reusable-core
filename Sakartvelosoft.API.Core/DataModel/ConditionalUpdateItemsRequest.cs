using Sakartvelosoft.API.Core.Filters;
using System.Collections.Generic;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class ConditionalUpdateItemsRequest<T> where T: class, new()
    {
        public DataFilter<T> Filter { get; set; }

        public List<EntityPropertyChange> Changes { get; set; }
    }
}