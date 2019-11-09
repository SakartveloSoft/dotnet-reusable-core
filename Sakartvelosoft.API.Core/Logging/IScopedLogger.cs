using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Logging
{
    public interface IScopedLogger<TScope> : ILogger where TScope: class
    {
        TScope Scope { get; }
    }
}
