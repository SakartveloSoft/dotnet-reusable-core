using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IAPIResponder
    {
        Task<IAPIResult> Done();
        Task<IAPIResult> Next();
        Task<IAPIResult> Ok<T>(T result) where T: class, new();
        Task<IAPIResult> JSON<T>(T result) where T : class, new();

        Task<IAPIResult> NoContent();
        Task<IAPIResult> ServerError(Exception err);
        Task<IAPIResult> BadRequest(Exception err);
        Task<IAPIResult> AccessDenied(Exception err);

    }
}
