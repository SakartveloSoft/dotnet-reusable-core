using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sakartvelosoft.API.Core.DataModel;
using Sakartvelosoft.API.Core.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Tests
{
    class Obj : IEntityWithKey<string>
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class FiltersTest
    {
        [TestMethod]
        public void TestFiltersConstruction()
        {
            {
                var propertyRef = Filters.Property<Obj, string, decimal>(obj => obj.Balance);
                Assert.AreEqual(propertyRef.NodeType, FilterNodeType.Property);
                var comparison = propertyRef > 4m;
                Assert.AreEqual(comparison.NodeType, FilterNodeType.Compare);
                var logicalOp = comparison && Filters.Property<Obj, string, string>(obj => obj.Name) == "Test";
                Assert.AreEqual(logicalOp.NodeType, FilterNodeType.BinaryLogicOp);
                var param = Filters.Parameter<string>("nameValue");
            }
            var builder = Filters.Builder<Obj, string>();

            {
                var propertyRef = builder.Property<decimal>(obj => obj.Balance);
                Assert.AreEqual(propertyRef.NodeType, FilterNodeType.Property);
                var comparison = propertyRef > 4m;
                Assert.AreEqual(comparison.NodeType, FilterNodeType.Compare);
                var logicalOp = comparison && builder.Property<string>(obj => obj.Name) == "Test";
                Assert.AreEqual(logicalOp.NodeType, FilterNodeType.BinaryLogicOp);
                var param = builder.Parameter<string>("nameValue");
                Assert.AreEqual(param.NodeType, FilterNodeType.Parameter);
                var eq = param == builder.Property(a => a.Name);
                Assert.AreEqual(eq.NodeType, FilterNodeType.Compare);


            }




        }

        [TestMethod]
        public void TestParametersBagParsing()
        {
            var bag = new
            {
                prop1 = true,
                prop2 = "string",
                prop3 = 1
            };

            var parsedBag = Filters.ParseValuesBag(bag);
            Assert.AreEqual(bag.prop1, parsedBag["prop1"]);
            Assert.AreEqual(bag.prop2, parsedBag["prop2"]);
            Assert.AreEqual(bag.prop3, parsedBag["prop3"]);
        }

    }
}
