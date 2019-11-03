using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.DataAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class NoIndexingAttribute: Indexing
    {
        NoIndexingAttribute() : base(IndexingModel.NoIndexing) { }
    }
}
