using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IHandlerWithInitialization
    {
        Task Initialize(APIInvocationContext context);
    }
}
