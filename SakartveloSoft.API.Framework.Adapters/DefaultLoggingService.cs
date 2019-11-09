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

        public LoggingContext Context => GetRootLogger().Context;

        private ILogger rootLogger;

        private List<Action<LoggingContext, LogMessage>> listeningCallbacks = new List<Action<LoggingContext, LogMessage>>();
        
        public ILogger GetRootLogger()
        {
            if (rootLogger == null)
            {
                rootLogger = new LoggerAdapter(LoggingContext.Empty, WriteMessage);
            }
            return rootLogger;
        }

        public Task Initialize(IGlobalServicesContext context)
        {
            rootLogger = new LoggerAdapter(LoggingContext.Empty.CreateSubContext(null, 
                new {
                    context.ApplicationId,
                    context.EnvironmentId,
                    context.ServiceId
            }), WriteMessage);
            this.Ready = true;
            return Task.CompletedTask;
        }

        public void Write(LogMessage message)
        {
            WriteMessage(GetRootLogger().Context, message);
        }

        public ILogger CreateSubLogger(string subName, object properties = null)
        {
            return GetRootLogger().CreateSubLogger(subName, properties);
        }

        public IScopedLogger<TScope> CreateSubLogger<TScope>(string name, TScope scope = null) where TScope : class
        {
            return GetRootLogger().CreateSubLogger(name, scope);
        }

        public IScopedLogger<TScope> CreateSubLogger<TScope>(TScope scope = null) where TScope : class
        {
            return GetRootLogger().CreateSubLogger(scope);
        }

        private void WriteMessage(LoggingContext context, LogMessage message)
        {
            for(var p = 0; p < listeningCallbacks.Count; p++)
            {
                try
                {
                    listeningCallbacks[p](context, message);
                } 
                catch(Exception e)
                {
                    Console.Error.WriteLine(e.ToString());
                }
            }
        }

        public void AddListener(Action<LoggingContext, LogMessage> messagesListener)
        {
            this.listeningCallbacks.Add(messagesListener);
        }
    }
}
