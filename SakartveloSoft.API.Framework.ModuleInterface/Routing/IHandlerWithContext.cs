using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IHandlerWithContext
    {
        public APIInvocationContext Context { get; set; }
    }
}
