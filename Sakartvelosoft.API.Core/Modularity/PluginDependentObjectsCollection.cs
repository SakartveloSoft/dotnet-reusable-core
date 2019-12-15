using SakartveloSoft.API.Framework.ModuleInterface.Modularity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Modularity
{
    public class PluginDependentObjectsCollection<T>: List<T> where T:  IPluginInjectable
    {
        public void PluginRemoved(IPluginReference plugin)
        {
            RemoveElementsFromPlugin(pluginId: plugin.Id);
        }

        public void RemoveElementsFromPlugin(string pluginId)
        {
            var itemsToRemove = new List<T>();
            for(var p = 0; p < Count; p++)
            {
                var item = this[p];
                if (item.PluginId == pluginId)
                {
                    itemsToRemove.RemoveAt(p);
                    p--;
                }
            }
        }

        public void PluginReplaced(IPluginReference plugin)
        {
            RemoveElementsFromPlugin(plugin.Id);
        }

    }
}
