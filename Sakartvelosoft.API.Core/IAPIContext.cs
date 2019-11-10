using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SakartveloSoft.API.Core
{
    public interface IAPIContext
    {
        string RequestId { get; set; }
        string RequestMethod { get; set; }
        string RequestUrl { get; set; }
        string ApplicationId { get; set; }
        string ConfigurationName { get; set; }
        IConfigurationReader ConfigurationReader { get; set; }
        string UserId { get; set; }
        string SessionId { get; set; }
        DateTime StartTime { get; set; }
        int StatusCode { get; set; }
        string ResponseContentType { get; set; }
        DateTime? ProcessingCompletedAt { get; set; }
        TimeSpan? TimeSpent { get; set; }
    }
}
