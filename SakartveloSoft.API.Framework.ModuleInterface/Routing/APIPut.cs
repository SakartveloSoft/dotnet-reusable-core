using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class APIPut : Attribute, IAPIMethod
    {
        public HttpMethod Method { get; set; }
        public string UrlTemplate { get; set; }
        public APIPut(string urlTemplate = null)
        {
            UrlTemplate = urlTemplate;
            Method = HttpMethod.Put;
        }
    }
}
