using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.DataModel
{
    public class GetItemsListRequest<T>: DataRequest<GetItemsListResponse<T>> where T: class, IEntityWithKey, new()
    {
        public List<string> Keys { get; set; }
    }
}
