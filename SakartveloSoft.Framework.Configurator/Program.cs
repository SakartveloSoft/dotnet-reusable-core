using CommandLine;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace SakartveloSoft.Framework.Configurator
{
    class Program
    {
        static void Main(string[] args)
        {
            var parseResult = Parser.Default.ParseArguments<ConfigurationGetCommand, ConfigurationSetCommand, ConfigurationDeleteCommand>(args);
            var output = parseResult.MapResult<ConfigurationGetCommand, ConfigurationSetCommand, ConfigurationDeleteCommand, Task<int>>(
                (ConfigurationGetCommand cmd) => cmd.Execute(),
                (ConfigurationSetCommand cmd) => cmd.Execute(),
                (ConfigurationDeleteCommand cmd) => cmd.Execute(),
                err => Task.FromResult(1)
            );
            output.ContinueWith(task =>
            {
                Environment.Exit(task.Result);
            });
        }
    }
}
