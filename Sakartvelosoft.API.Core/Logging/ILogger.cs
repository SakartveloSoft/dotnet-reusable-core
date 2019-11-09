using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public interface ILogger
    {
        public LoggingContext Context { get; }
        void Write(LogMessage message);

        ILogger CreateSubLogger(string subName, object properties = null);

        IScopedLogger<TScope> CreateSubLogger<TScope>(string name, TScope scope = null) where TScope : class;

        IScopedLogger<TScope> CreateSubLogger<TScope>(TScope scope = null) where TScope: class;
    }
}
