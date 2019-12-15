using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface.Routing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple =false, Inherited =true)]
    public sealed class APIUrl : Attribute
    {
        public string UrlTemplate { get; set; }

        public APIUrl(string urlTemplate)
        {
            UrlTemplate = urlTemplate;
        }
    }
}
