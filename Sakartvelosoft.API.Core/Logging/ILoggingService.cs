using Sakartvelosoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Logging
{
    public interface ILoggingService : IGlobalService
    {
        public ILogger GetRootLogger();
        public ILogger CreateScopedLogger(params string[] names);
    }
}
