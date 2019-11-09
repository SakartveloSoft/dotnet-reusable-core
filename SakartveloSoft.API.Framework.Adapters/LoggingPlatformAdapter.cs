using Microsoft.Extensions.Logging;
using SakartveloSoft.API.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class LoggingPlatformAdapter : ILoggerProvider, ILoggingServiceProxy
    {
        public ILoggingService AppLoggingService { get; set; }
        public bool Disposed { get; private set; }
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return new PlatformLoggerAdapter(this, categoryName);
        }

        public LoggingSeverity MinSeverity { get; private set; } = LoggingSeverity.Debugging;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool v)
        {
            Disposed = true;
        }

        internal bool IsSeverityEnabled(LoggingSeverity loggingSeverity)
        {
            return (int)loggingSeverity >= (int)MinSeverity;
        }

        public LoggingPlatformAdapter SetMinSeverity(LoggingSeverity minSeverity)
        {
            MinSeverity = minSeverity;
            return this;
        }
    }
}
