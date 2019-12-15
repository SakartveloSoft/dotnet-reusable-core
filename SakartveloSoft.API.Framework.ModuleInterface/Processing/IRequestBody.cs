using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Processing
{
    public interface IRequestBody
    {
        int ContentLength { get; set; }
        string ContentType { get; set; }
    }
}
