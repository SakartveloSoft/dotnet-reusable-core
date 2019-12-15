using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Framework.ModuleInterface
{
    interface IConfigurationReader
    {
        string this[string name]
        {
            get;
            set;
        }
    }
}
