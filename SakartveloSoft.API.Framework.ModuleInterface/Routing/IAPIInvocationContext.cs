using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IAPIInvocationContext
    {
        public APIRequest Request { get; set; }
        public APIResponse Response { get; set; }
    }
}
