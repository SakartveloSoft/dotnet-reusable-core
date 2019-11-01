using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class GetSingleItemRequest<T, TKey> : DataRequest<SingleValueDataResponse<T>> where T : class, IEntityWithKey<TKey>, new()
    {
        public TKey Key { get; internal set; }
    }
}
