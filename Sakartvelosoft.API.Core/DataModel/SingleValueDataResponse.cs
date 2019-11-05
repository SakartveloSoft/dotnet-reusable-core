using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.DataModel
{
    public class SingleValueDataResponse<T>: DataResponse
    {
        public T Result { get; set; }
    }
}
