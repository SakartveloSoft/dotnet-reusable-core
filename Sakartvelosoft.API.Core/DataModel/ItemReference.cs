using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public struct ItemReference<T> where T: class, new() 
    {
        public readonly string Key;

        public ItemReference(string value) : this()
        {
            this.Key = value;
        }
    }
}
