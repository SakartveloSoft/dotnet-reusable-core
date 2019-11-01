using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public abstract class DataRequest<ResponseT> where ResponseT: DataResponse
    {
        public string RequestId { get; set; }
        public string DataRequestId { get; set; }
        public DateTime Started { get; set; }

        public string DataOperationName { get; set; }

        public ResponseT Response { get; set; }

    }
}
