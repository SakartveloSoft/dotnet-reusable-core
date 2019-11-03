using System;
using System.Collections.Generic;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class EntityChangesPack<T> where T : class, IEntityWithKey, new() 
    {
        public string Key { get; set; }
        public List<EntityPropertyChange> Updates { get; set; }
    }
}