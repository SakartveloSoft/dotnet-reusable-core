using SakartveloSoft.API.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class ScopedLoggerAdapter<TScope> : IScopedLogger<TScope> where TScope: class
    {
        public LoggingContext<TScope> Context { get; private set; }
        public ScopedLoggerAdapter(LoggingContext<TScope> loggingContext, Action<LoggingContext,LogMessage> messagesWriter)
        {
            Context = loggingContext;
            Scope = loggingContext.Scope;
            this.messagesWriter = messagesWriter;
        }

        public TScope Scope { get; private set; }

        LoggingContext ILogger.Context => Context;

        private Action<LoggingContext, LogMessage> messagesWriter;

        public void Write(LogMessage message)
        {
            messagesWriter(Context, message);
        }

        public ILogger CreateSubLogger(string subName, object properties = null)
        {
            return new LoggerAdapter(Context.CreateSubContext(subName, properties), messagesWriter);
        }

        public IScopedLogger<TScope1> CreateSubLogger<TScope1>(string name, TScope1 scope = default) where TScope1 : class
        {
            return new ScopedLoggerAdapter<TScope1>(Context.CreateSubContext(name, scope), messagesWriter);
        }

        public IScopedLogger<TScope1> CreateSubLogger<TScope1>(TScope1 scope = default) where TScope1 : class
        {
            return new ScopedLoggerAdapter<TScope1>(Context.CreateSubContext(scope), messagesWriter);
        }
    }
}
