using Microsoft.VisualStudio.TestTools.UnitTesting;
using SakartveloSoft.API.Metadata;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Tests
{
    [TestClass]
    public class TestBase36
    {
        [TestMethod]
        public void TestBase36Formatting()
        {
            var rnd = new Random();
            var buf = new byte[8];
            rnd.NextBytes(buf);
            var val = BitConverter.ToUInt64(buf.AsSpan());
            var result = Base36.ToBase36(val);
            Console.WriteLine($@"Base36 of {val} is {result} with {result.Length} characters");
        }
        [TestMethod]
        public void TestBase62IdFormatting()
        {
            var manager = new MetadataManager();
            int x;
            for (x = 0; x < 100; x++)
            {
                var newId = manager.GeneratePrefixedRandomId("Test");
                Console.WriteLine($@"Generated {newId} with {newId.Length} characters");
            }
            Console.WriteLine($@"Generated random {x} long prefixed ids");
            for (x = 0; x < 100; x++)
            {
                var newId = manager.GeneratePrefixedShortRandomId("Test");
                Console.WriteLine($@"Generated {newId} with {newId.Length} characters");
            }
            Console.WriteLine($@"Generated random {x} long prefixed ids");
        }
    }
}
