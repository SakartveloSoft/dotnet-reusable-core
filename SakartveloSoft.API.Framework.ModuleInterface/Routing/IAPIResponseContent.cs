using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    public interface IAPIResponseContent
    {
        public long ContentLength { get; }
        public string ContentType { get; }
        public Task WriteToStream(Stream stream);
    }
}
