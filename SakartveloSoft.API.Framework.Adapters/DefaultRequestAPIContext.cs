using SakartveloSoft.API.Core;
using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class DefaultRequestAPIContext : IAPIContext
    {
        public DefaultRequestAPIContext(IConfigurationReader conf)
        {
            ConfigurationReader = conf;
        }
        public string RequestId { get; set; }
        public HttpMethod RequestMethod { get; set; }
        public string RequestUrl { get; set; }
        public string ApplicationId { get; set; }
        public string ConfigurationName { get; set; }
        public IConfigurationReader ConfigurationReader { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public DateTime StartTime { get; set; }
        public int StatusCode { get; set; }
        public string ResponseContentType { get; set; }
        public DateTime? ProcessingCompletedAt { get; set; }
        public TimeSpan? TimeSpent { get; set; }
        string IAPIContext.RequestMethod { get; set; }
    }
}
