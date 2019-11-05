using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Services
{
    public interface IGlobalServicesContext
    {
        bool Has<TService>() where TService: IGlobalService;
        TService Get<TService>() where TService : IGlobalService;
        string EnvironmentId { get; }
        string ApplicationId { get; }
        string VersionId { get; }

        IReadOnlyDictionary<string, string> Environment { get; }
    }
}
