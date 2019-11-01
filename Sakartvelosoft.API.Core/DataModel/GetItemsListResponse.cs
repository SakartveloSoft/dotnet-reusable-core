using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class GetItemsListResponse<T, TKey>: DataResponse where T: class, IEntityWithKey<TKey>, new()
    {
        public List<T> Items { get; set; }
    }
}
