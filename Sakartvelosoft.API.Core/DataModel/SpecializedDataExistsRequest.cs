using SakartveloSoft.API.Core.Filtering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.DataModel
{
    public class SpecializedDataExistsRequest<T>: DataExistsRequest where T: class, IEntityWithKey, new()
    {
        public DataFilter<T> Filter { get; set; }
        public string Key { get;  set; }
    }
}
