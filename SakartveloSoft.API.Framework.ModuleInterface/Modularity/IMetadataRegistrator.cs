using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Modularity
{
    public interface IMetadataRegistrator
    {
        public void DiscoverType<T>();
        public void DiscoverType(Type type);
        public void DiscoverAssembly(Assembly assembly);
    }
}
