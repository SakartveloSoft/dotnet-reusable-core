using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public class LogMessage
    {
        public string Message { get; set; }
        public LoggingSeverity Severity { get; set; }
        public DateTime EventTime { get; set; }
        public string OperationName { get; set; }
        public JToken Details { get; set; }

        public JToken FailureDetails { get; set; }

        public LogMessage()
        {
            EventTime = DateTime.UtcNow;
        }
    }
}
