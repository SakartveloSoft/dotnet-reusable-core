using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Services
{
    public interface IServicesContext
    {
        T Get<T>();
        object Get(Type type);
    }
}
