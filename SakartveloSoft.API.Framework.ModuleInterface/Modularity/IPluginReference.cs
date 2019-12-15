using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Modularity
{
    public interface IPluginReference
    {
        string Id { get;  }
        string Name { get; }
        string Description { get; }
        string IconUrl { get; }

        Task<PluginCommandResult<TResult>> SendCommand<TResult>(PluginCommand<TResult> command);
        Task RegisterMetaTypes(IMetadataRegistrator typesRegistrator);


    }
}
