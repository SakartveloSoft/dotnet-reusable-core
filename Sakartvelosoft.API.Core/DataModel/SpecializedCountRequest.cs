using SakartveloSoft.API.Core.Filtering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.DataModel
{
    public class SpecializedCountRequest<T> : CountDataRequest where T: class, IEntityWithKey, new()
    { 
        public DataFilter<T> Filter { get; set; }
    }
}
