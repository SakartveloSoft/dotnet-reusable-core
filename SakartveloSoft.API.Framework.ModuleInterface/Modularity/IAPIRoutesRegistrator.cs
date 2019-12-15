using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Modularity
{
    public interface IAPIRoutesRegistrator
    {
        void RegisterAPIHandler<T>(string urlPrefix = null) where T: class, new();
        void RegisterAPIRoute(HttpMethod method, string urlTemplate, APISecurityModel securityModel, Func<APIInvocationContext, Func<IAPIResponseContent, Task<APIInvocationEffect>>, Task<APIInvocationEffect>> handler);
        void RegisterAPIRoute(HttpMethod method, string urlTemplate, APISecurityModel securityModel, Func<APIInvocationContext, Func<Task>, Task> handler);
    }
}
