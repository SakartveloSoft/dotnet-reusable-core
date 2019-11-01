using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public interface IEntityWithKey<TKey>
    {
        public TKey Id { get; set; }
    }
}
