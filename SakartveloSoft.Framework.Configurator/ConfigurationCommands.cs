using CommandLine;
using Microsoft.Extensions.Configuration;
using SakartveloSoft.API.Core.Configuration;
using SakartveloSoft.Configuration.CosmosDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.Framework.Configurator
{
    public abstract class ConfigurationCommand
    {
        [Option("env",Default = "local", HelpText = "Environment Id", MetaValue = "ENVIRONMENT", Required = false)]
        public string EnvironmentId { get; set; }
        [Option("component", Default = "", HelpText = "Component Id to narrow configuration scope")]
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

        protected virtual async Task<IConfigurationManipulator> GetConfigurationManipulator()
        {

            var manipulator = new ConfigurationManipulator();
            manipulator.EnvironmentId = EnvironmentId;
            manipulator.ConnectionString = this.Configuration.GetConnectionString("DatabaseEndpoint");
            manipulator.DatabaseName = Configuration["appSettings:configurationDatabase"];
            return await manipulator.Initialize();
        }

        protected virtual IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder();
            return builder
                .AddJsonFile(Path.Combine(Environment.CurrentDirectory, $@"appSettings.json"), optional:true)
                .AddJsonFile(Path.Combine(Environment.CurrentDirectory, $@"appSettings_{EnvironmentId}.json"))
                .AddEnvironmentVariables()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "EnvironmentId", EnvironmentId }
                })
                .Build();

        }
    }

    [Verb("list", HelpText = "List direct subnodes (or whole subtree) of values at optional prefix")]
    public class ConfigurationListCommand : ConfigurationCommand {
        [Option(Default =false, HelpText = "List also subvalues of subvalues at specified path")]
        public bool Recursive { get; set; }
        public override async Task<int> DoIt()
        {
            var manipulator = await GetConfigurationManipulator();
            var subNodes = await manipulator.GetEntries(Component, ConfigurationPath, recursive:Recursive, forPages:false);
            IEnumerable<string> result;
            if (Recursive)
            {
                result = subNodes.Select(entry => entry.Path.ToString());
            } else
            {
                result = subNodes.Select(entry => entry.Path.Leaf);
            }
            foreach(var name in result)
            {
                Console.WriteLine(name);
            }
            return 0;
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
        [Option('h',"hide", Default = null)]
        bool HideFromClient { get; set; }
        [Option('s', "show", Default = false)]
        bool ShowToClient { get; set; }
        [Value(2, Default ="string", HelpText = "Value type to use - string, boolean, int, etc", Required =false)]
        public string ValueMeaning { get; set; }
        [Value(3, HelpText = "New value for setting", Required =false)]
        public string Value { get; set; }
        public string Label { get; set; }
        public override async Task<int> DoIt()
        {
            var manipulator = await GetConfigurationManipulator();
            if (ValueMeaning != null && Value != null) {
                var value = ParseFutureValue(Value, out ConfigurationValueMeaning meaning);
                var newEntry = await manipulator.EnsureForEntry(Component, ConfigurationPath, value, meaning, Label, DetectClientVisibility());
                Console.WriteLine(@$"Entry for {Component} Component  {newEntry.Path} has been updated");
            } else
            {
                var updatedEntry = await manipulator.EnsureForEntry(Component, ConfigurationPath, label: Label, visibleForPages: DetectClientVisibility());
                Console.WriteLine(@$"Options for entry of {Component} Component  {updatedEntry.Path} has been configures");
            }
            return 0;
        }

        private bool? DetectClientVisibility()
        {
            if (HideFromClient)
            {
                return false;
            }
            if (ShowToClient)
            {
                return true;
            }
            return default;
        }

        private ConfigurationValue ParseFutureValue(string value, out ConfigurationValueMeaning meaning)
        {
            if (Enum.TryParse(ValueMeaning, ignoreCase:true, out meaning))
            {
                switch(meaning)
                {
                    case ConfigurationValueMeaning.String:
                    case ConfigurationValueMeaning.APIPassword:
                    case ConfigurationValueMeaning.APIUserName:
                    case ConfigurationValueMeaning.Choice:
                        return value;
                    case ConfigurationValueMeaning.EmailAddress:
                        var parsedAddress = new MailAddress(value);
                        return parsedAddress.ToString();
                    case ConfigurationValueMeaning.Url:
                        var url = new Uri(value);
                        return url.ToString();
                    case ConfigurationValueMeaning.Flag:
                        return TryParseAdvancedBoolean(value);
                    case ConfigurationValueMeaning.Date:
                        return DateTime.Parse(value);
                    case ConfigurationValueMeaning.Integer:
                        return int.Parse(value);
                    case ConfigurationValueMeaning.Float:
                        return double.Parse(value);
                    case ConfigurationValueMeaning.Bytes:
                        return Convert.FromBase64String(value);
                }
            }
            throw new ApplicationException("Unknown value meanng " + ValueMeaning);
        }

        private bool TryParseAdvancedBoolean(string value)
        {
            if ("true|yes|ok|y|1".Contains(value.Trim().ToLower()))
            {
                return true;
            }
            if ("false|no|cancel|n|0".Contains(value.Trim().ToLower()))
            {
                return false;
            }
            throw new ArgumentException("Invalid boolean/flag value: " + value);
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
