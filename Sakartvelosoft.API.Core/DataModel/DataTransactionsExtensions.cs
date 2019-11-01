using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Sakartvelosoft.API.Core.DataModel
{
    public static class DataTransactionsExtensions
    {
        public static IDataChangesTransaction DeleteItem<T, TKey>(this IDataChangesTransaction transaction, TKey key) where T : class, IEntityWithKey<TKey>, new()
        {
            return transaction.DeleteItems(new DeleteItemRequest<T, TKey>()
            {
                DataOperationName = "DeleteItems",
                Key = key
            });
        }
        public static IDataChangesTransaction DeleteItems<T, TKey>(this IDataChangesTransaction transaction, IEnumerable<T> items) where T : class, IEntityWithKey<TKey>, new()
        {
            return transaction.DeleteItems(new DeleteItemRequest<T, TKey>()
            {
                Keys = items.Select(item => item.Id).ToList()
            });
        }

        public static IDataChangesTransaction DeleteItems<T, TKey>(this IDataChangesTransaction transaction, params T[] items) where T : class, IEntityWithKey<TKey>, new() {
            return transaction.DeleteItems(new DeleteItemRequest<T, TKey>()
            {
                Keys = items.Select(item => item.Id).ToList()
            });
        }

    }
}
