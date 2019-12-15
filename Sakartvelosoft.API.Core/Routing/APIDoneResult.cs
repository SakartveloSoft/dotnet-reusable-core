using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    public class APIDoneResult: APIResult
    {
        private APIDoneResult()
        {
            CallEffect = Framework.ModuleInterface.Routing.APIInvocationEffect.Done;
        }
        public override Task SendContentToStream(Stream target)
        {
            return Task.CompletedTask;
        }

        public static readonly APIDoneResult DefaultValue = new APIDoneResult();
    }
}
