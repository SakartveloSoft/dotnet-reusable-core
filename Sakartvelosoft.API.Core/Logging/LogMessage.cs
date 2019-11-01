﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Logging
{
    public class LogMessage
    {
        public string Message { get; set; }
        public LoggingSeverity Severity { get; set; }
        public string OperationName { get; set; }
        public System.Text.Json.JsonDocument Details { get; set; }

        public System.Text.Json.JsonDocument FailureDetails { get; set; }
    }
}
