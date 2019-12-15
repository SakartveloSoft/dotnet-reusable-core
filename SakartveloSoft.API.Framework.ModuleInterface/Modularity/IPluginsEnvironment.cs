using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Modularity
{
    interface IPluginsEnvironment
    {
        Task<bool> IsPluginPresent(string pluginId, Version minVersion = null);
        Task<PluginCommandResult<TResult>> SendCommand<TResult>(IPluginReference reference, PluginCommand<TResult> command);
        Task<IList<IPluginReference>> GetKnownPlugins();
        Task<IPluginReference> GetPluginReference(string name);
    }
}
