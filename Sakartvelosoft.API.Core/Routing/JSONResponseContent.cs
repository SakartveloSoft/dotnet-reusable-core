using SakartveloSoft.API.Framework.ModuleInterface.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Routing
{
    public class JSONResponseContent<T> : IAPIResponseContent
    {

        private MemoryStream buffer;

        public JSONResponseContent(T content)
        {
            buffer = new MemoryStream();
            using (var writer = new System.Text.Json.Utf8JsonWriter(buffer))
            {
                System.Text.Json.JsonSerializer.Serialize<T>(writer, content);
            }
            buffer.Position = 0;
            ContentLength = buffer.Length;
            ContentType = "application/json;charset=UTF-8;";
        }

        public long ContentLength { get; private set; }
        public string ContentType { get; private set; }

        public Task WriteToStream(Stream stream)
        {
            buffer.Position = 0;
            return buffer.CopyToAsync(stream);
        }
    }
}
