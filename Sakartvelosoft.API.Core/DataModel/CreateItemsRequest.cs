using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class CreateItemsRequest<T, TKey> where T: class, IEntityWithKey<TKey>, new()
    {
    }
}
