using Sakartvelosoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sakartvelosoft.API.Core.Configuration
{
    public interface IConfigurationService: IGlobalService
    {
        void AddProvider(IConfigurationLoader loader);

    }
}
