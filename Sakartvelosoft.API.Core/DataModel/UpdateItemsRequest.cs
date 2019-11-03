using System.Collections.Generic;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class UpdateItemsRequest<T> where T: class, IEntityWithKey,  new()
    {
        public List<EntityChangesPack<T>> Changes { get; set; }
    }
}