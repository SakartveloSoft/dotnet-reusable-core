using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sakartvelosoft.API.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Tests
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class ValuesTest
    {
        [TestMethod]
        public void TestValuesFromDynamic()
        {
            var dynamicEntry = ConfigurationValue.FromJSON(@"{ 
    ""value1"": true, ""value2"": 1, ""value3"": ""Some text"" 
    }");
            Assert.AreEqual(1, dynamicEntry.DynamicValue.value2);
            Assert.AreEqual("Some text", dynamicEntry.DynamicValue.value3);
            Assert.IsTrue(dynamicEntry.DynamicValue.value1);
        }
    }
}
