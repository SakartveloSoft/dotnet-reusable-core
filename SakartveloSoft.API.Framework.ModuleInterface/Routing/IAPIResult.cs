using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IAPIResult
    {
        APIInvocationEffect CallEffect { get; }
        HttpStatusCode StatusCode { get; }
        IAPIResponseContent Content { get; }
        Task SendContentToStream(Stream target);

    }
}
