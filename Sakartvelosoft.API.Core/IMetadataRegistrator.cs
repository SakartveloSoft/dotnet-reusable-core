using SakartveloSoft.API.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core
{
    public interface IMetadataRegistrator
    {
        public MetaType EnsureTypeDiscovered<T>();
    }
}
