using System.Collections.Generic;
using System.Net;

namespace SakartveloSoft.API.Framework.ModuleInterface.Modularity
{
    public interface IPluginInvocationError
    {
        string Message { get; }
        string CodeLocation { get;  }
        HttpStatusCode StatusCode { get; }
        IReadOnlyDictionary<string, object> Details { get; }
    }
}