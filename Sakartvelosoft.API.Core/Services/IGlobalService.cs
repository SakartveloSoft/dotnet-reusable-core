using Sakartvelosoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sakartvelosoft.API.Core.Services
{
    public interface IGlobalService
    {
        public IConfigurationReader Configuration { get; set; }
        public Task WaitForUsableState();

        public bool Ready { get; }

        public Task Initialize(IGlobalServicesContext context);
    }
}
