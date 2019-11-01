using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sakartvelosoft.API.Core.DataModel
{
    public interface IDataChangesTransaction: IDisposable
    {
        IDataChangesTransaction CreateItems<T, TKey>(CreateItemsRequest<T, TKey> request) where T : class, IEntityWithKey<TKey>, new();
        IDataChangesTransaction UpdateItems<T, TKey>(UpdateItemsRequest<T, TKey> request) where T : class, IEntityWithKey<TKey>, new();
        IDataChangesTransaction ApplyConditionalUpdate<T, TKey>(ConditionalUpdateItemsRequest<T, TKey> request) where T : class, IEntityWithKey<TKey>, new();
        IDataChangesTransaction DeleteItems<T, TKey>(DeleteItemRequest<T, TKey> request) where T : class, IEntityWithKey<TKey>, new();

        Task Commit();
        Task Abort();
    }
}
