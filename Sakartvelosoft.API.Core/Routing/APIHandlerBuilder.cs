using SakartveloSoft.API.Framework.ModuleInterface.Modularity;
using SakartveloSoft.API.Core.Routing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SakartveloSoft.API.Framework.ModuleInterface.Routing;

namespace SakartveloSoft.API.Core.Routing
{
    public class APIHandlerBuilder
    {
        public string PluginId { get; set; }
        public Version PluginVersion { get; set; }
        public virtual List<IAPIRoute> BuildHandler<T>() where T: class, new()
        {
            var type = typeof(T);
            var attributes = type.GetCustomAttributes(true);
            var urlPrefix = default(string);
            var defaultSecurity = APISecurityModel.Public;
            foreach (var attr in attributes)
            {
                if (attr is APIUrl)
                {
                    urlPrefix = (attr as APIUrl).UrlTemplate;
                } else if (attr is APISecurity)
                {
                    defaultSecurity = (attr as APISecurity).SecurityModel;
                }
            }
            List<IAPIRoute> entries = new List<IAPIRoute>();
            foreach(var actionMethod in type.GetMethods())
            {
                var methodSecurity = defaultSecurity;
                var methodVerb = default(HttpMethod);
                var urlSuffix = default(string);
                foreach(var attr in actionMethod.GetCustomAttributes(true))
                {
                    if (attr is IAPIMethod)
                    {
                        var methodData = attr as IAPIMethod;
                        urlSuffix = methodData.UrlTemplate;
                        methodVerb = methodData.Method;

                    } else if (attr is APIUrl)
                    {
                        urlSuffix = (attr as APIUrl).UrlTemplate;
                    } else if (attr is APISecurity)
                    {
                        methodSecurity = (attr as APISecurity).SecurityModel;
                    }
                    if (methodVerb == null)
                    {
                        continue;
                    }

                }
                entries.Add(BuildMethodHandler(method: methodVerb, urlTemplate: CombineUrlTemplate(urlPrefix, urlSuffix), methodSecurity, type, actionMethod));

            }
            return entries;
        }

        private IAPIRoute BuildMethodHandler(HttpMethod method, string urlTemplate, APISecurityModel methodSecurity, Type type, MethodInfo actionMethod)
        {
            return new APIRoute(method, urlTemplate)
            {
                Method = method,
                SecurityModel = methodSecurity,
                PluginId = PluginId,
                PluginVersion = PluginVersion
                
            };

            

        }

        private string CombineUrlTemplate(string urlPrefix, string urlSuffix)
        {
            if (string.IsNullOrWhiteSpace(urlPrefix) && string.IsNullOrWhiteSpace(urlSuffix))
            {
                return string.Empty;
            }
            if (string.IsNullOrWhiteSpace(urlSuffix))
            {
                return urlPrefix.Trim();
            }
            if (string.IsNullOrWhiteSpace(urlPrefix))
            {
                return urlSuffix.Trim();
            }
            if (urlPrefix.EndsWith('/'))
            {
                urlPrefix = urlPrefix.Substring(0, urlPrefix.Length - 1);
            }
            if (urlSuffix.StartsWith('/'))
            {
                urlSuffix = urlSuffix.Substring(1);
            }
            return $@"{urlPrefix}/{urlSuffix}";
        }
    }
}
