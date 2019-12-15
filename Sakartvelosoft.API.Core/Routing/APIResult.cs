using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    public abstract class APIResult: IAPIResult
    {
        public APIInvocationEffect CallEffect { get; protected set; }
        public HttpStatusCode StatusCode { get; protected set; } 
        public IAPIResponseContent Content { get; protected set; }
        public abstract Task SendContentToStream(Stream target);
    }
}
