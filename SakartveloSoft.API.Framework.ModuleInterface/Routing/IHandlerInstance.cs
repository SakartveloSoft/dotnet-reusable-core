using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IHandlerInstance
    {
        public Task HandleRequest(APIInvocationContext context, IAPIResponder responder);
    }
}
