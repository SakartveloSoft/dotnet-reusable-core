using Microsoft.VisualStudio.TestTools.UnitTesting;
using SakartveloSoft.API.Core.Routing;
using SakartveloSoft.API.Framework.ModuleInterface.Processing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SakartveloSoft.API.Core.Tests
{
    [TestClass]
    public class UrlTemplateTesting
    {
        [TestMethod]
        public void TestRouteMatching()
        {
            var testRoute = new APIRoute(HttpMethod.Get, "/test/{id}");
            var match1 = testRoute.TryMatch(new APIRequest(HttpMethod.Get, "/test/1234"));
            Assert.IsNotNull(match1);
            Assert.IsTrue(match1.ContainsKey("id"));
            Assert.AreEqual(match1["id"], "1234");

        }
    }
}
