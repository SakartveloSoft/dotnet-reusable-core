using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class FromQueryString : Attribute, IParameterAttribute
    {
        public ParameterSource Source { get; set; }
        public string Name { get; set; }
        public FromQueryString(string name = null)
        {
            Name = name;
            Source = ParameterSource.QueryString;
        }
    }
}
