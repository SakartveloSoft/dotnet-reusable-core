using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public interface ILogWriter
    {
        void WriteMessage(LoggingPath path, LogMessage message);
    }
}
