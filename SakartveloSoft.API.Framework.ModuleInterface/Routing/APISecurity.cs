using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple =false, Inherited =true)]
    public sealed class APISecurity : Attribute
    {
        public APISecurityModel SecurityModel { get; set; }

        public APISecurity(APISecurityModel securityModel)
        {
            SecurityModel = securityModel;
        }
    }
}
