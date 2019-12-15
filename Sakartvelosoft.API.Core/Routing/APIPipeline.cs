using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    public class APIPipeline : IHandlerInstance
    {
        private List<APIHandlerFunc> steps = new List<APIHandlerFunc>();

        public async Task HandleRequest(APIInvocationContext context, IAPIResponder responder)
        {
            foreach(var step in steps)
            {
                var result = await step(context, responder);
                switch(result.CallEffect)
                {
                    case APIInvocationEffect.Done:
                        break;
                    case APIInvocationEffect.ContinueProcessing:
                        continue;
                    case APIInvocationEffect.ReturnResponse:
                        context.Response.StatusCode = result.StatusCode;
                        context.Response.Content = result.Content;
                        break;

                }
            }
        }
    }
}
