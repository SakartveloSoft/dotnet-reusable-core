using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Logging
{
    public static class LoggingExtensions
    {
        public static void Write(this ILogger logger, LoggingSeverity severity, string message, params object[] args)
        {
            logger.Write(new LogMessage
            {
                Severity = severity,
                Message = String.Format(message, args)
            });
        }

        public static void Information(ILogger logger, string message, params object[] args)
        {
            logger.Write(LoggingSeverity.Information, message, args);
        }

        public static void Warning(ILogger logger, string message, params object[] args)
        {
            logger.Write(LoggingSeverity.Warning, message, args);
        }

        public static void Error(ILogger logger, string message, params object[] args)
        {
            logger.Write(LoggingSeverity.Error, message, args);
        }
    }
}
