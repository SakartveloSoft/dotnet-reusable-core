using CommandLine;
using Microsoft.Extensions.Configuration;
using SakartveloSoft.API.Core.Configuration;
using SakartveloSoft.Configuration.CosmosDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.Framework.Configurator
{
    public abstract class ConfigurationCommand
    {
        [Option("env",Default = "staging", HelpText = "Environment Id", MetaValue = "ENVIRONMENT", Required = false)]
        public string EnvironmentId { get; set; }
        [Option("component", Default = null, HelpText = "Component Id to narrow configuration scope")]
        public string Component { get; set; }
        [Value(1, Required = true, MetaName = "Configuration Path", MetaValue = "PATH.IN.CONFIG")]
        public string ConfigurationPath { get; set; }

        public IConfiguration Configuration { get; set; }

        public Task<int> Execute()
        {
            if (this.Configuration == null)
            {
                Configuration = BuildConfiguration();
                Console.WriteLine($@"Target configuration for {EnvironmentId} environment and {Component ?? "root"} component");
            }
            return DoIt();
        }

        public virtual Task<int> DoIt()
        {
            Console.WriteLine($@"Executing {ToString()} command");
            foreach(var pair in Configuration.AsEnumerable())
            {
                Console.WriteLine($@"{pair.Key} = {pair.Value}");
            }
            return Task.FromResult(0);
        }

        protected virtual IConfigurationManipulator GetConfigurationManipulator()
        {

            var manipulator = new ConfigurationManipulator();
            manipulator.ConnectionString = this.Configuration.GetConnectionString("DatabaseEndpoint");
            manipulator.DatabaseName = Configuration["DatabaseName"];
            return manipulator;
        }

        protected virtual IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();
            return builder.AddJsonFile(Path.Combine(Environment.CurrentDirectory, $@"appSettings_{EnvironmentId}.json"))
                .AddEnvironmentVariables()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "EnvironmentId", EnvironmentId }
                })
                .Build();

        }
    }

    [Verb("get", HelpText = "Gets value from configuration if present")]
    public class ConfigurationGetCommand : ConfigurationCommand
    {
        public override Task<int> DoIt()
        {
            return base.DoIt();
        }
    }

    [Verb("set", HelpText = "Updates value in configuration")]
    public class ConfigurationSetCommand : ConfigurationCommand
    {
        [Option('h',"hide", Default = false)]
        bool ShowToClient { get; set; }
        [Option('s', "show", Default = false)]
        bool HideFromClient { get; set; }
        [Value(1, Default ="string", HelpText = "Value type to use - string, boolean, int, etc")]
        public string ValueType { get; set; }
        [Value(2, HelpText = "New value for setting")]
        public string Value { get; set; }
        public string ValueMeaning { get; set; }
        public string Label { get; set; }
        public override async Task<int> DoIt()
        {
            var manipulator = GetConfigurationManipulator();
            var meaning = ConfigurationValueMeaning.String;
            var value = ParseFutureValue(ValueType, Value, out meaning);
            var newEntry = await manipulator.EnsureForEntry(Component, ConfigurationPath, value, meaning, Label, DetectClientVisibility());
            return 0;
        }

        private bool? DetectClientVisibility()
        {
            throw new NotImplementedException();
        }

        private ConfigurationValue ParseFutureValue(string valueType, string value, out ConfigurationValueMeaning meaning)
        {
            throw new NotImplementedException();
        }
    }

    [Verb("delete", HelpText = "Removes value from configuration if present")]
    public class ConfigurationDeleteCommand : ConfigurationCommand
    {
        public override Task<int> DoIt()
        {
            return base.DoIt();
        }
    }


}
