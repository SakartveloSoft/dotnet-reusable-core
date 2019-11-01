using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class GetItemsListRequest<T, TKey>: DataRequest<GetItemsListResponse<T, TKey>> where T: class, IEntityWithKey<TKey>, new()
    {
        public List<T> Results { get; set; }
        public List<TKey> Keys { get; internal set; }
    }
}
