using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SakartveloSoft.API.Core.DataModel
{
    public static class DataTransactionsExtensions
    {
        public static IDataChangesTransaction DeleteItem<T>(this IDataChangesTransaction transaction, string key) where T : class, IEntityWithKey, new()
        {
            return transaction.DeleteItems(new DeleteItemRequest<T>()
            {
                DataOperationName = "DeleteItems",
                Key = key
            });
        }
        public static IDataChangesTransaction DeleteItems<T>(this IDataChangesTransaction transaction, IEnumerable<T> items) where T : class, IEntityWithKey, new()
        {
            return transaction.DeleteItems(new DeleteItemRequest<T>()
            {
                Keys = items.Select(item => item.Id).ToList()
            });
        }

        public static IDataChangesTransaction DeleteItems<T>(this IDataChangesTransaction transaction, params T[] items) where T : class, IEntityWithKey, new() {
            return transaction.DeleteItems(new DeleteItemRequest<T>()
            {
                Keys = items.Select(item => item.Id).ToList()
            });
        }

    }
}
