using Sakartvelosoft.API.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class SpecializedDataExistsRequest<T, TKey>: DataExistsRequest where T: class, IEntityWithKey<TKey>, new()
    {
        public DataFilter<T, TKey> Filter { get; set; }
        public TKey Key { get;  set; }
    }
}
