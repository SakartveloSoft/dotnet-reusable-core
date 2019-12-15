using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Processing
{
    public class APIInvocationContext : IAPIInvocationContext
    {
        public APIRequest Request { get; set; }
        public APIResponse Response { get; set; }
    }
}
