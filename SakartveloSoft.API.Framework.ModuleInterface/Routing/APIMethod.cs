using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class APIMethod: Attribute, IAPIMethod
    {
        public HttpMethod Method { get; set; }
        public string UrlTemplate { get; set; }
        public APIMethod(HttpMethod method, string urlTemplate = null)
        {
            Method = method;
            UrlTemplate = urlTemplate;
        }
    }
}
