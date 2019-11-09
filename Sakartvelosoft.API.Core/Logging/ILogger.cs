using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public interface ILogger
    {
        public LoggingPath Path { get; }
        void Write(LogMessage message);

        ILogger CreateSubLogger(params string[] subNames);

        IScopedLogger<TScope> CreateSubLogger<TScope>(TScope scope) where TScope: class;
    }
}
