using SakartveloSoft.API.Core.Configuration;
using SakartveloSoft.API.Core.Logging;
using SakartveloSoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public class DefaultLoggingService : ILoggingService
    {
        public IConfigurationReader Configuration { get; set; }

        public bool Ready { get; private set; }

        private ILogger rootLogger;

        public ILogger CreateScopedLogger(params string[] names)
        {
            return rootLogger.CreateSubLogger(names);
        }

        public IScopedLogger<TScope> CreateScopedLogger<TScope>(TScope scope) where TScope : class
        {
            return rootLogger.CreateSubLogger(scope);
        }

        public ILogger GetRootLogger()
        {
            if (rootLogger == null)
            {
                rootLogger = new LoggerAdapter(new LoggingPath(), WriteMessage);
            }
            return rootLogger;
        }

        public Task Initialize(IGlobalServicesContext context)
        {
            rootLogger = new LoggerAdapter(new LoggingPath(context.ApplicationId, context.EnvironmentId, context.ServiceId), WriteMessage);
            this.Ready = true;
            return Task.CompletedTask;
        }

        public void WriteMessage(LoggingPath path, LogMessage message)
        {

        }

        public IScopedLogger<TScope> CreateLoggerForScope<TScope>(TScope scope) where TScope : class
        {
            return new ScopedLoggerAdapter<TScope>(LoggingPath.Empty, scope, WriteMessage);
        }
    }
}
