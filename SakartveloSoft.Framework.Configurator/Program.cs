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
            var parseResult = Parser.Default.ParseArguments<ConfigurationListCommand, ConfigurationGetCommand, ConfigurationSetCommand, ConfigurationDeleteCommand, ConfigurationNewKeyCommand>(args);
            var output = parseResult.MapResult(
                (ConfigurationGetCommand cmd) => cmd.Execute(),
                (ConfigurationSetCommand cmd) => cmd.Execute(),
                (ConfigurationDeleteCommand cmd) => cmd.Execute(),
                (ConfigurationListCommand cmd) => cmd.Execute(),
                (ConfigurationNewKeyCommand cmd) => cmd.Execute(),
                err =>
                {
                    foreach(var e in err)
                    {
                        Console.Error.WriteLine(e);
                    }
                    return Task.FromResult(1);
                }
            );
            output.ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Console.Error.WriteLine(task.Exception);
                    Environment.Exit(-1);
                }
                else
                {
                    Environment.Exit(task.Result);
                }
            }).Wait();
        }
    }
}
