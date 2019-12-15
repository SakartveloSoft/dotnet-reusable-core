using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class FromRoute : Attribute, IParameterAttribute
    {
        public ParameterSource Source { get; set; }
        public string Name { get; set; }
        public FromRoute(string name = null) 
        {
            Name = name;
            Source = ParameterSource.Route;
        }
    }
}
