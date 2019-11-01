using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.DataModel
{
    public abstract class DataResponse
    {
        public string RequestId { get; set; }
        public string DataRequestId { get; set; }
        public DateTime WhenStarted { get; set; }
        public DateTime WhenCompleted { get; set; }
        public TimeSpan TimeSpent { get; set; }
    }
}
