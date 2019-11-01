using System.Collections.Generic;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class UpdateItemsRequest<T, Tkey> where T: class, IEntityWithKey<Tkey>,  new()
    {
        public List<EntityChangesPack<T, Tkey>> Changes { get; set; }
    }
}