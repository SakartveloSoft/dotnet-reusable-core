using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IAPIMethod
    {
        HttpMethod Method { get; set; }
        public string UrlTemplate { get; set; }
    }
}
