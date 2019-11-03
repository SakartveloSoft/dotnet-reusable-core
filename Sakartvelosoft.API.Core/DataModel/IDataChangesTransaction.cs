using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sakartvelosoft.API.Core.DataModel
{
    public interface IDataChangesTransaction: IDisposable
    {
        IDataChangesTransaction CreateItems<T>(CreateItemsRequest<T> request) where T : class, new();
        IDataChangesTransaction UpdateItems<T>(UpdateItemsRequest<T> request) where T : class, IEntityWithKey, new();
        IDataChangesTransaction ApplyConditionalUpdate<T>(ConditionalUpdateItemsRequest<T> request) where T : class, IEntityWithKey, new();
        IDataChangesTransaction DeleteItems<T>(DeleteItemRequest<T> request) where T : class, IEntityWithKey, new();

        Task Commit();
        Task Abort();
    }
}
