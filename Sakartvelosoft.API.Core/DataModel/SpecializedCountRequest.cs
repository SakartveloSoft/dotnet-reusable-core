using Sakartvelosoft.API.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class SpecializedCountRequest<T, TKey> : CountDataRequest where T: class, IEntityWithKey<TKey>, new()
    { 
        public DataFilter<T, TKey> Filter { get; set; }
    }
}
