using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SakartveloSoft.API.Core.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace SakartveloSoft.API.Framework.Adapters
{
    internal class PlatformLoggerAdapter : ILogger
    {
        private LoggingPlatformAdapter loggingPlatformAdapter;
        private string categoryName;

        private List<object> stateStack = new List<object>();
        

        public PlatformLoggerAdapter(LoggingPlatformAdapter loggingPlatformAdapter, string categoryName)
        {
            this.loggingPlatformAdapter = loggingPlatformAdapter;
            this.categoryName = categoryName;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            this.stateStack.Add(state);
            return new DisposableCallback(() =>
            {
                stateStack.Remove(state);
            });
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return loggingPlatformAdapter.IsSeverityEnabled(ConvertToSeverity(logLevel));
        }

        private LoggingSeverity ConvertToSeverity(LogLevel logLevel)
        {
            switch(logLevel)
            {
                case LogLevel.Information:
                    return LoggingSeverity.Information;
                case LogLevel.Debug:
                    return LoggingSeverity.Debugging;
                case LogLevel.Warning:
                    return LoggingSeverity.Warning;
                case LogLevel.Error:
                    return LoggingSeverity.Error;
                case LogLevel.Critical:
                    return LoggingSeverity.Critical;
                case LogLevel.Trace:
                    return LoggingSeverity.Debugging;
                case LogLevel.None:
                    return LoggingSeverity.Information;
                default:
                    return LoggingSeverity.Debugging;
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!this.IsEnabled(logLevel))
            {
                return;
            }
            var attributes = new LogMessage
            {
                Severity = ConvertToSeverity(logLevel),
                Message = formatter(state, exception),
                Details = new JObject()
            };
            if (eventId != null) {
                attributes.Details["EventId"] = eventId.Id;
                attributes.Details["EventIdName"] = eventId.Name;
            }
            if (state != null)
            {
                if (state is IReadOnlyList<KeyValuePair<string, object>>)
                {
                    foreach (var pair in ((IReadOnlyList<KeyValuePair<string, object>>)state))
                    {
                        if (pair.Value == null)
                        {
                            attributes.Details[pair.Key] = null;
                        }
                        else
                        {
                            var valType = pair.Value.GetType();
                            if (valType.IsValueType || valType.IsEnum || valType.IsPrimitive)
                            {
                                attributes.Details[pair.Key] = JToken.FromObject(pair.Value);
                            }
                            else if (pair.Value is Delegate || pair.Value is MulticastDelegate || pair.Value is MemberInfo || pair.Value is Type)
                            {
                                attributes.Details[pair.Key] = pair.Value.ToString();
                            }
                            else
                            {
                                attributes.Details[pair.Key] = JToken.FromObject(pair.Value);
                            }
                        }
                    }
                } 
                else 
                {
                    var type = typeof(TState);
                    if (type == typeof(string) || type.IsPrimitive || type.IsEnum)
                    {
                        attributes.Details["Text"] = state.ToString();
                    } else
                    {
                        attributes.Details["Values"] = JToken.FromObject(state);
                    }
                }
            } else
            {
                attributes.Details["NoDetais"] = true;
            }
            if (exception != null)
            {
                attributes.FailureDetails = JObject.FromObject(exception);
            }
            loggingPlatformAdapter.AppLoggingService.Write(attributes);
        }
    }
}