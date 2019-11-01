using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sakartvelosoft.API.Core.DataModel
{
    public interface IDatabase
    {
        Task<ListDataResponse<T, TKey>> Find<T, TKey>(DataListRequest<T, TKey> request) where T: class, IEntityWithKey<TKey>, new();
        Task<CountDataResponse> Count<T, TKey>(SpecializedCountRequest<T, TKey> request) where T : class, IEntityWithKey<TKey>, new();
        Task<IsExistsDataResponse> IsExists<T, TKey>(SpecializedDataExistsRequest<T, TKey> request) where T : class, IEntityWithKey<TKey>, new();


        Task<T> GetItem<T, TKey>(GetSingleItemRequest<T, TKey> request) where T : class, IEntityWithKey<TKey>, new();


        Task<GetItemsListResponse<T, TKey>> GetList<T, TKey>(GetItemsListRequest<T, TKey> request) where T : class, IEntityWithKey<TKey>, new();

        IDataChangesTransaction BeginDataUpdates();


        


        


    }
}
