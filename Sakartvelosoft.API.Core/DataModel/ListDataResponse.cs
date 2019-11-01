using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class ListDataResponse<T, TKey>: DataResponse where T: class, IEntityWithKey<TKey>, new()
    {
        public List<T> Items { get; set; }
        public bool PagingApplied { get; set; }
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public bool TotalItemsKnown { get; set; }
        public long? TotalItems { get; set; }
        public bool SortingApplied { get; set; }

        public bool? SortAscending { get; set; }
    }
}
