using SakartveloSoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public interface ILoggingService : IGlobalService
    {
        public ILogger GetRootLogger();
        public ILogger CreateScopedLogger(params string[] names);

        public ILogger CreateScopedLogger<TScope>(TScope scope) where TScope: INamedScope;
    }
}
