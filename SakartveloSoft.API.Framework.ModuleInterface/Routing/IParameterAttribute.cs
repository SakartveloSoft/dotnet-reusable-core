using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IParameterAttribute
    {
        ParameterSource Source { get; }
        string Name { get; }
    }
}
