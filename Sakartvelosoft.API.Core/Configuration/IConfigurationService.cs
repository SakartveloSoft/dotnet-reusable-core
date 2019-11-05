using SakartveloSoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Configuration
{
    public interface IConfigurationService: IGlobalService
    {
        void AddProvider(IConfigurationLoader loader);

    }
}
