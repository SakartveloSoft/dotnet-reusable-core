using SakartveloSoft.API.Core.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class LoggerAdapter: ILogger
    {
        public LoggingPath Path { get; private set; }

        private Action<LoggingPath, LogMessage> messagesWriter;

        public LoggerAdapter(LoggingPath path, Action<LoggingPath, LogMessage> messagesWriter)
        {
            Path = path;
            this.messagesWriter = messagesWriter;
        }

        public ILogger CreateSubLogger(params string[] subNames)
        {
            return new LoggerAdapter(Path.Append(subNames), messagesWriter);
        }

        public ILogger CreateSubLogger<TScope>(TScope scope) where TScope : ILoggingScope
        {
            return new LoggerAdapter(Path.Append(scope.Name), messagesWriter);
        }

        public void Write(LogMessage message)
        {
            messagesWriter(Path, message);
        }
    }
}
