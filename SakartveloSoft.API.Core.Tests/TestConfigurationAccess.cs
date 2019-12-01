using Microsoft.VisualStudio.TestTools.UnitTesting;
using SakartveloSoft.Configuration.CosmosDB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SakartveloSoft.API.Core.Tests
{
    [TestClass]
    public class TestConfigurationAccess
    {
        [TestMethod]
        public async Task TestConfigCreateAndUpdate()
        {
            var confAccessor = new ConfigurationManipulator();
            confAccessor.ConnectionString = "mongodb://localhost:27017";
            confAccessor.DatabaseName = "localconfiguration";
            confAccessor.SettingsCollection = "settings";

            await confAccessor.Initialize();

            var conf = await confAccessor.GetEntry(null, "test");

            if (conf != null)
            {
                await confAccessor.EnsureForEntry(null, "test", "test value", Configuration.ConfigurationValueMeaning.String, "Test", true);
            }
            var newVal = "Updated string value " + DateTime.Now.ToString("u");
            conf = await confAccessor.EnsureForEntry(null, "test", newVal);

            Assert.AreEqual(newVal, conf.Value.StringValue, "String value does not match");

        }
    }
}
