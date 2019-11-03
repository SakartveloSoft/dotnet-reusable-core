using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public class Reference
    {
        public static ItemReference<T> FromKey<T>(string value) where T: class, new()
        {
            return new ItemReference<T>(value);
        } 
    }
}
