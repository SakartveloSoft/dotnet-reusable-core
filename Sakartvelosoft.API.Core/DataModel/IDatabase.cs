using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sakartvelosoft.API.Core.DataModel
{
    public interface IDatabase
    {
        Task<ListDataResponse<T>> Find<T>(DataListRequest<T> request) where T: class, IEntityWithKey, new();
        Task<CountDataResponse> Count<T>(SpecializedCountRequest<T> request) where T : class, IEntityWithKey, new();
        Task<IsExistsDataResponse> IsExists<T>(DataListRequest<T> request) where T : class, IEntityWithKey, new();
        Task<IsExistsDataResponse> IsExists<T>(SpecializedDataExistsRequest<T> request) where T : class, IEntityWithKey, new();


        Task<T> GetItem<T, TKey>(GetSingleItemRequest<T> request) where T : class, IEntityWithKey, new();


        Task<GetItemsListResponse<T>> GetList<T>(GetItemsListRequest<T> request) where T : class, IEntityWithKey, new();

        IDataChangesTransaction BeginDataUpdates();


        


        


    }
}
