using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.DataModel
{
    public static class DatabaseExtensions
    {
        public static Task<T> Get<T>(this IDatabase db, string id) where T : class, IEntityWithKey, new()
        {
            return db.GetItem<T, string>(new GetSingleItemRequest<T>()
            {
                Key = id
            });
        }

        public async static Task<List<T>> GetList<T>(this IDatabase db, IEnumerable<string> keys) where T : class, IEntityWithKey, new()
        {
            var reply = await db.GetList(new GetItemsListRequest<T>()
            {
                Keys = keys.ToList()

            });
            return reply.Items;
        }

        public async static Task<List<T>> GetList<T>(this IDatabase db, params string[] keys) where T: class, IEntityWithKey, new() 
        {
            var reply = await db.GetList(new GetItemsListRequest<T>()
            {
                Keys = keys.ToList()
            });
            return reply.Items;
        }

        public static async Task<bool> IsExists<T>(this IDatabase db, string key) where T: class, IEntityWithKey, new()
        {
            var reply = await db.IsExists(new SpecializedDataExistsRequest<T>()
            {
                Key = key
            });
            return reply.Result;
        }
    }
}
