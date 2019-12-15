using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public sealed class APIParameterAttribute : Attribute, IParameterAttribute
    {
        public string Name { get; set; }
        public ParameterSource Source { get; set; }
        public APIParameterAttribute(ParameterSource source, string name = null)
        {
            Source = source;
            Name = name;
        }
    }
}
