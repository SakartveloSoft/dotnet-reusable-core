using SakartveloSoft.API.Core.Filtering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.DataModel
{
    public class DataListRequest<T> : DataSearchRequest<ListDataResponse<T>> where T: class, new()
    {
        public DataFilter<T> Filter { get; set; }

        public string Keywords { get; set; }

        public int? PageSize { get; set; }

        public long? PageIndex { get; set; }

        public bool PagingApplied
        {
            get
            {
                return this.PageSize.HasValue && PageSize.Value > 0;
            }
        }

        public bool? SortAscending { get; set; }

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
