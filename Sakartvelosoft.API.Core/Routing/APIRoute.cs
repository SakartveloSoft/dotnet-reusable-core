using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Template;
using SakartveloSoft.API.Framework.ModuleInterface.Modularity;
using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    public class APIRoute : IAPIRoute
    {
        public string PluginId { get; set; }
        public Version PluginVersion { get; set; }
        public string RoutePath { get; set; }

        public HttpMethod Method { get; set; }

        public APISecurityModel SecurityModel { get; set; }
        public RouteTemplate UrlTemplate { get; set; }

        public Func<APIInvocationContext, Func<APIInvocationContext, Task<APIInvocationEffect>, APIInvocationEffect>> Handler { get; set; }

        public RouteTemplate ParsedTemplate => UrlTemplate;

        string IAPIRoute.UrlTemplate => RoutePath;

        public APIPipeline Pipeline { get; private set; }

        private TemplateMatcher matcher;

        public APIRoute()
        {

        }

        public APIRoute(HttpMethod method, string urlTemplate)
        {
            Method = method;
            RoutePath = urlTemplate;
            UrlTemplate = TemplateParser.Parse(urlTemplate);
        }

        public APIRoutingResult DetectRoutingMatch(APIInvocationContext context)
        {
            var routeParameters = TryMatch(context.Request);
            if (routeParameters == null)
            {
                return null;
            }
            return new APIRoutingResult
            {
                Route = this as IAPIRoute,
                Values = routeParameters
            };
        }

        public IDictionary<string, object> TryMatch(APIRequest request)
        {
            if (request.Method != Method)
            {
                return null;
            }
            //implemented using https://blog.markvincze.com/matching-route-templates-manually-in-asp-net-core/
            if (matcher == null)
            {
                var defaults = GetDefaultsForTemplate();
                matcher = new TemplateMatcher(UrlTemplate, defaults);
            }
            var routeValues = new RouteValueDictionary();
            if (!matcher.TryMatch(request.Path, routeValues))
            {
                return null;
            }
            return routeValues;
        }

        private RouteValueDictionary GetDefaultsForTemplate()
        {
            var results = new RouteValueDictionary();
            foreach (var entry in UrlTemplate.Parameters)
            {
                if (entry.DefaultValue != null)
                {
                    results.Add(entry.Name, entry.DefaultValue);
                }
            }
            return results;
        }
    }
}
