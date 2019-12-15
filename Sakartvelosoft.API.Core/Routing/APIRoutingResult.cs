using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    public class APIRoutingResult
    {
        public IAPIRoute Route { get; set; }
        public IDictionary<string, object> Values { get; set; }
        Func<APIInvocationContext, Func<APIInvocationContext, Task<APIInvocationEffect>>, Task<APIInvocationEffect>> Handler { get; set; } 
    }
}
