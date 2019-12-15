using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Processing
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public APIHeadersBag Headers { get; set; }
        public IAPIResponseContent Content { get; set; }
    }
}
