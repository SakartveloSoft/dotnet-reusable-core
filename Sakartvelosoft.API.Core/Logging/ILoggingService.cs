using SakartveloSoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public interface ILoggingService : IGlobalService, ILogger
    {
        void AddListener(Action<LoggingContext, LogMessage> messagesListener);
    }
}
