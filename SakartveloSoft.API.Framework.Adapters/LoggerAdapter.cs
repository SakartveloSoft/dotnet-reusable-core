using SakartveloSoft.API.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class LoggerAdapter: ILogger
    {
        public LoggingContext Context { get; private set; }

        private Action<LoggingContext, LogMessage> messagesWriter;

        public LoggerAdapter(LoggingContext context, Action<LoggingContext, LogMessage> messagesWriter)
        {
            Context = context;
            this.messagesWriter = messagesWriter;
        }

        public ILogger CreateSubLogger(string subName)
        {
            return new LoggerAdapter(Context.CreateSubContext(subName), messagesWriter);
        }

        public IScopedLogger<TScope> CreateSubLogger<TScope>(TScope scope) where TScope : class
        {
            return new ScopedLoggerAdapter<TScope>(Context.CreateSubContext<TScope>(scope), messagesWriter);
        }

        public void Write(LogMessage message)
        {
            messagesWriter(Context, message);
        }

        public ILogger CreateSubLogger(string subName, object properties = null)
        {
            return new LoggerAdapter(Context.CreateSubContext(subName, properties), messagesWriter);
        }

        public IScopedLogger<TScope> CreateSubLogger<TScope>(string name, TScope scope = null) where TScope : class
        {
            return new ScopedLoggerAdapter<TScope>(Context.CreateSubContext(name, scope), messagesWriter);
        }
    }
}
