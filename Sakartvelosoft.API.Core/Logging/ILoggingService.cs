using SakartveloSoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public interface ILoggingService : IGlobalService
    {
        ILogger GetRootLogger();
        ILogger CreateScopedLogger(params string[] names);

        IScopedLogger<TScope> CreateScopedLogger<TScope>(TScope scope) where TScope: class;

        IScopedLogger<TScope> CreateLoggerForScope<TScope>(TScope scope) where TScope: class;
    }
}
