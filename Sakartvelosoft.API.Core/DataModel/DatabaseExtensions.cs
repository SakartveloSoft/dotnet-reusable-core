using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakartvelosoft.API.Core.DataModel
{
    public static class DatabaseExtensions
    {
        public static Task<T> Get<T, TKey>(this IDatabase db, TKey id) where T : class, IEntityWithKey<TKey>, new()
        {
            return db.GetItem<T, TKey>(new GetSingleItemRequest<T, TKey>()
            {
                Key = id
            });
        }

        public async static Task<List<T>> GetList<T, TKey>(this IDatabase db, IEnumerable<TKey> keys) where T : class, IEntityWithKey<TKey>, new()
        {
            var reply = await db.GetList(new GetItemsListRequest<T, TKey>()
            {
                Keys = keys.ToList()

            });
            return reply.Items;
        }

        public async static Task<List<T>> GetList<T, TKey>(this IDatabase db, params TKey[] keys) where T: class, IEntityWithKey<TKey>, new() 
        {
            var reply = await db.GetList(new GetItemsListRequest<T, TKey>()
            {
                Keys = keys.ToList()
            });
            return reply.Items;
        }

        public static async Task<bool> IsExists<T,TKey>(this IDatabase db, TKey key) where T: class, IEntityWithKey<TKey>, new()
        {
            var reply = await db.IsExists(new SpecializedDataExistsRequest<T, TKey>()
            {
                Key = key
            });
            return reply.Result;
        }
    }
}
