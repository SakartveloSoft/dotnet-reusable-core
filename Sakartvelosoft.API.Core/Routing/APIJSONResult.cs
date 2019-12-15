using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    public class APIJSONResult<T>: APIResult where T: class, new()
    {
        public APIJSONResult(T content, HttpStatusCode status)
        {
            StatusCode = status;
            Content = new JSONResponseContent<T>(content);
        }

        public override Task SendContentToStream(Stream target)
        {
            return Content.WriteToStream(target);
        }
    }
}
