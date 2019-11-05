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
        public IConfigurationReader Configuration { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool Ready => throw new NotImplementedException();

        private ILogger rootLogger;

        public ILogger CreateScopedLogger(params string[] names)
        {
            return new LoggerAdapter(new LoggingPath(names), WriteMessage);
        }

        public ILogger CreateScopedLogger<TScope>(TScope scope) where TScope : ILoggingScope
        {
            throw new NotImplementedException();
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
            return Task.CompletedTask;
        }

        public void WriteMessage(LoggingPath path, LogMessage message)
        {

        }

    }
}
