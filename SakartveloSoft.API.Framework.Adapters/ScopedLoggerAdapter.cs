using SakartveloSoft.API.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class ScopedLoggerAdapter<TScope> : LoggerAdapter, IScopedLogger<TScope> where TScope: class
    {
        public ScopedLoggerAdapter(LoggingPath loggingPathParent, TScope scope, Action<LoggingPath,LogMessage> messagesWriter):
            base((loggingPathParent ?? LoggingPath.Empty).Append(scope is ILoggingScope ?( scope as ILoggingScope).Name : scope.ToString()), messagesWriter)
        {
            Scope = scope;
        }

        public TScope Scope { get; private set; }
    }
}
