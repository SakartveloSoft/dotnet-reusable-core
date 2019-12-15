using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Processing
{
    public class APIRequest
    {
        public HttpMethod Method { get; set; }
        public string Path { get; set; }
        public IDictionary<string, string> Query { get; set; }
        public APIHeadersBag Headers { get; set; }

        public IRequestBody Body { get; set; }

        public APIRequest()
        {

        }
        public APIRequest(HttpMethod method, string path)
        {
            Method = method;
            Path = path;
        }
    }
}
