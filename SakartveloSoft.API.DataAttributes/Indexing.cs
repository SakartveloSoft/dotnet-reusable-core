using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.DataAttributes
{
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Property)]
    public abstract class Indexing: Attribute
    {
        public Indexing(IndexingModel indexingModel)
        {
            Model = indexingModel;
        }

        public IndexingModel Model { get; private set; }
    }
}
