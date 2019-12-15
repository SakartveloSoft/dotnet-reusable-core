using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Modularity
{
    public abstract class PluginCommand<TResult>
    {
        public string Id { get; set; }
        public DateTimeOffset SentAt { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
    }
}
