using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class DeleteItemRequest<T, TKey>: DataRequest<NoResultResponse> where T: class, IEntityWithKey<TKey>, new()
    {
        public TKey Key { get; set; }
        public List<TKey> Keys { get; set; }

        public Boolean IsBatchDelete { get; set; }
    }
}
