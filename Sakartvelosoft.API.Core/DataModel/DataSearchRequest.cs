using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.DataModel
{
    public abstract class DataSearchRequest<TResponse>: DataRequest<TResponse> where TResponse: DataResponse, new()
    {

    }
}
