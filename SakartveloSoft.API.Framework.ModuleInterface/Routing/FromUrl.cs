using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class FromUrl : Attribute, IParameterAttribute
    {
        public ParameterSource Source { get; set; }
        public string Name { get; set; }
        public FromUrl(string url = null) 
        {
            Name = Name;
            Source = ParameterSource.Url;
        }
    }
}
