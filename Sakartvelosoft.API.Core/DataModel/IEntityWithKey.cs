using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.DataModel
{
    public interface IEntityWithKey
    {
        public string Id { get; set; }
    }
}
