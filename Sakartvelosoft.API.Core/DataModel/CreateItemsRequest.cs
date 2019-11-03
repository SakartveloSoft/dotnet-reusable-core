using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class CreateItemsRequest<T> where T: class, new()
    {
        public List<T> Items { get; set; }
    }
}
