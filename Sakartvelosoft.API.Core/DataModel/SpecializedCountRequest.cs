using Sakartvelosoft.API.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class SpecializedCountRequest<T> : CountDataRequest where T: class, IEntityWithKey, new()
    { 
        public DataFilter<T> Filter { get; set; }
    }
}
