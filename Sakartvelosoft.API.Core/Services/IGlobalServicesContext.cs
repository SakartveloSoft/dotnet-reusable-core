using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Services
{
    public interface IGlobalServicesContext
    {
        bool Has<TService>() where TService: class, IGlobalService;
        TService Get<TService>() where TService : class, IGlobalService;
        string EnvironmentId { get; }
        string ApplicationId { get; }
        string ServiceId { get; }
        string VersionId { get; }

        IReadOnlyDictionary<string, string> Environment { get; }

        void UpdateGlobalOptions(string applicationName, string environmentName, string webRootPath, string contentRootPath);

        Task Initialize();
    }
}
