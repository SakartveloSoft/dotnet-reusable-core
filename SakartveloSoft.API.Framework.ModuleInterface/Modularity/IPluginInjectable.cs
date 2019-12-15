using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Modularity
{
    public interface IPluginInjectable
    {
        string PluginId { get; }
        Version PluginVersion { get; }
    }
}
