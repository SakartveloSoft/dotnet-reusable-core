using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class DeleteItemRequest<T>: DataRequest<NoResultResponse> where T: class, IEntityWithKey, new()
    {
        public string Key { get; set; }
        public List<string> Keys { get; set; }

        public bool IsBatchDelete { get; set; }
    }
}
