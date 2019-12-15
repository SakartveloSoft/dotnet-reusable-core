using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Modularity
{
    public class PluginCommandResult<T>
    {
        public T Result { get; set; }
        bool IsFailure { get; set; }
        public IPluginInvocationError FailureDetails { get; set; }
    }
}
