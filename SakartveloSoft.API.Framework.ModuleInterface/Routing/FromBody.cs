using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class FromBody: Attribute, IParameterAttribute
    {
        public ParameterSource Source { get; set; }
        public string Name { get; set; }
        public FromBody() 
        {
            Source = ParameterSource.Body;
        }
    }
}
