using SakartveloSoft.API.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public static class DefaultConsoleLogger
    {
        public static void WriteLogMessageToConsole(LoggingContext ctx, LogMessage message)
        {
            var builder = new StringBuilder().Append(message.EventTime.ToString("yyyy-MM-ddTHH:mm:ss.ffffzzz"))
                .Append($@" [{message.Severity.ToString().Substring(0, 4).ToUpper()}] ");
            var path = ctx.ComponentsPath;
            if (path != null && path.Count > 0)
            {
                for (var p = 0; p < path.Count; p++)
                {
                    if (p > 0)
                    {
                        builder.Append("/");
                    }
                    builder.Append(path[p]);
                }
            }
            var contextValues = ctx.FormattedValuesList;
            if (contextValues.Count > 0)
            {
                builder.AppendLine().Append(" in context of ");
                for (var p = 0; p < contextValues.Count; p++)
                {
                    var val = contextValues[p];
                    if (p > 0)
                    {
                        builder.Append(", ");
                    }
                    builder.Append(val.Name).Append("=").Append(val.Value ?? "null");
                }
                builder.AppendLine();
            }
            if (message.OperationName != null)
            {
                builder = builder.Append(@$" inside ({message.OperationName})");
            }
            builder.AppendLine().Append("  ").Append(message.Message);
            if (message.Details != null)
            {
                builder = builder.AppendLine().Append(message.Details.ToString(Newtonsoft.Json.Formatting.None));
            }

            Console.WriteLine(builder);

        }
    }
}
