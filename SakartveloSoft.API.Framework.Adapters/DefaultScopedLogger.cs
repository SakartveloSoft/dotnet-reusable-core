using SakartveloSoft.API.Core.Logging;

namespace SakartveloSoft.API.Framework.Adapters
{
    internal class DefaultScopedLogger<T>: IScopedLogger<T> where T: class
    {
        private readonly ILoggingService logService;
        private readonly LoggingContext context;

        public DefaultScopedLogger(ILoggingService logService, Microsoft.AspNetCore.Http.HttpContext httpContext, T scope = null)
        {
            this.logService = logService;
            this.Scope = scope;
            this.impl = logService.CreateSubLogger(scope);
            this.context = impl.Context;

        }

        public LoggingContext<T> Context => throw new System.NotImplementedException();

        public T Scope { get; }

        private IScopedLogger<T> impl;

        LoggingContext ILogger.Context => context;

        public ILogger CreateSubLogger(string subName, object properties = null)
        {
            return this.impl.CreateSubLogger(subName, properties);
        }

        public IScopedLogger<TScope> CreateSubLogger<TScope>(string name, TScope scope = null) where TScope : class
        {
            return this.impl.CreateSubLogger(name, scope);
        }

        public IScopedLogger<TScope> CreateSubLogger<TScope>(TScope scope = null) where TScope : class
        {
            return this.impl.CreateSubLogger(scope);
        }

        public void Write(LogMessage message)
        {
            this.impl.Write(message);
        }
    }
}