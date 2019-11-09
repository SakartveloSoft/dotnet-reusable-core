using SakartveloSoft.API.Core.Configuration;
using SakartveloSoft.API.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Framework.Adapters
{
    public abstract class GlobalServiceBase : IGlobalService
    {
        public virtual IConfigurationReader Configuration { get; set; }

        public bool Ready { get; private set; }

        public async Task Initialize(IGlobalServicesContext context)
        {
            if (!Ready)
            {
                await PerformInitialization(context);
            }
            Ready = true;
        }

        protected abstract Task PerformInitialization(IGlobalServicesContext context);

    }
}
