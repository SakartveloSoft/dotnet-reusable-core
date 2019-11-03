using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Sakartvelosoft.API.Core.DataModel
{
    public static class DataSearchHelper
    {
        public static DataListRequest<T> ParseSearchRequest<T>(JsonElement element) where T: class, new()
        {
            var result = new DataListRequest<T>();
            foreach(var prop in element.EnumerateObject()) {
                switch(prop.Name)
                {
                    case "pageIndex":
                        result.PageIndex = prop.Value.GetInt32();
                        break;
                    case "pageSize":
                        result.PageSize = prop.Value.GetInt32();
                        break;
                    case "sortBy":
                        result.SortOption = prop.Value.GetString();
                        break;
                    case "ascending":
                        result.SortAscending = prop.Value.GetBoolean();
                        break;
                    case "keywords":
                        result.Keywords = prop.Value.GetString();
                        break;
                    case "filter":
                    case "filters":
                        result.Filter = Filters.Filters.Builder<T>().FromJSON(prop.Value);
                        break;
                    default:
                        throw new ArgumentException("Unknown parameter " + prop.Name);
                }
            }
        }
    }
}
