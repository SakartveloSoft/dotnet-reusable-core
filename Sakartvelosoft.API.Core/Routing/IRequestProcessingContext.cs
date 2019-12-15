using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    interface IRequestProcessingContext
    {
        Task ProcessResponse(APIRequest request, APIResponse response);
    }
}
