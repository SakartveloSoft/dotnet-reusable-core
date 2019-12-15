using Microsoft.AspNetCore.Routing.Template;
using SakartveloSoft.API.Framework.ModuleInterface.Modularity;
using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IAPIRoute : IPluginInjectable
    {
        HttpMethod Method { get; }
        string UrlTemplate { get; }
        RouteTemplate ParsedTemplate { get; }
        IDictionary<string, object> TryMatch(APIRequest request);
        
    }
}
