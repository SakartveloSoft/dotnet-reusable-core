using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class APIPost: Attribute, IAPIMethod
    {
        public HttpMethod Method { get; set; }
        public string UrlTemplate { get; set; }
        public APIPost(string urlTemplate = null)
        {
            UrlTemplate = urlTemplate;
            Method = HttpMethod.Post;
        }
    }
}
