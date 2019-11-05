using SakartveloSoft.API.Core.Filtering;
using System.Collections.Generic;

namespace SakartveloSoft.API.Core.DataModel
{
    public class ConditionalUpdateItemsRequest<T> where T: class, new()
    {
        public DataFilter<T> Filter { get; set; }

        public List<EntityPropertyChange> Changes { get; set; }
    }
}