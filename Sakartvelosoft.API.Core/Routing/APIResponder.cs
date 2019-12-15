using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    public class APIResponder : IAPIResponder
    {

        public Task<IAPIResult> Done()
        {
            return Task.FromResult(APIDoneResult.DefaultValue as IAPIResult);
        }

        public Task<IAPIResult> JSON<T>(T result) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IAPIResult> Next()
        {
            throw new NotImplementedException();
        }

        public Task<IAPIResult> NoContent()
        {
            throw new NotImplementedException();
        }

        public Task<IAPIResult> Ok<T>(T result) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public Task<IAPIResult> ServerError(Exception err)
        {
            throw new NotImplementedException();
        }

        public Task<IAPIResult> AccessDenied(Exception err)
        {
            throw new NotImplementedException();
        }

        public Task<IAPIResult> BadRequest(Exception err)
        {
            throw new NotImplementedException();
        }

    }
}
