using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.DataModel
{
    public class GetSingleItemRequest<T> : DataRequest<SingleValueDataResponse<T>> where T : class, IEntityWithKey, new()
    {
        public string Key { get; set; }
    }
}
