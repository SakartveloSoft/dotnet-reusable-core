using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public enum APISecurityModel
    {
        Public,
        RequireSignIn,
        RequireSubscription,
        RequireAdministrativeAccess
    }
}
