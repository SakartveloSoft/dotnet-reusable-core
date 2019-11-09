using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public interface ILoggingServiceProxy
    {
        public ILoggingService AppLoggingService { get; set; }
    }
}
