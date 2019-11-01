using Sakartvelosoft.API.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class DataListRequest<T, TKey> : DataSearchRequest<ListDataResponse<T, TKey>> where T: class, IEntityWithKey<TKey>, new()
    {
        DataFilter<T, TKey> Filter { get; set; }

        public int? PageSize { get; set; }

        public long? PageIndex { get; set; }

        public bool PagingApplied
        {
            get
            {
                return this.PageSize.HasValue && PageSize.Value > 0;
            }
        }

        bool? SortAscending { get; set; }

        public string SortOption { get; set; }

        public bool SortingApplied
        {
            get
            {
                return SortOption != null && SortOption.Length > 0;
            }
        }
    }
}
