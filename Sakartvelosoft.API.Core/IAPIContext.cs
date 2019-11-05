using SakartveloSoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core
{
    public interface IAPIContext
    {
        string RequestId { get; set; }
        string ApplicationId { get; set; }
        string ConfigurationName { get; set; }
        IConfigurationReader ConfigurationReader { get; set; }
        string UserId { get; set; }
    }
}
