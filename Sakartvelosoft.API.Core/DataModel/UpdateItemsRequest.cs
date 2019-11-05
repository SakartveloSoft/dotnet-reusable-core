using System.Collections.Generic;

namespace SakartveloSoft.API.Core.DataModel
{
    public class UpdateItemsRequest<T> where T: class, IEntityWithKey,  new()
    {
        public List<EntityChangesPack<T>> Changes { get; set; }
    }
}