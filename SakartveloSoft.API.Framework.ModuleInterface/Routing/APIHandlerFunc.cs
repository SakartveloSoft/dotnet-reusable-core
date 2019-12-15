using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public delegate Task<IAPIResult> APIHandlerFunc(APIInvocationContext context, IAPIResponder responder);
}
