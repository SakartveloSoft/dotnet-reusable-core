using Microsoft.VisualStudio.TestTools.UnitTesting;
using SakartveloSoft.API.Core.DataModel;
using SakartveloSoft.API.Core.Filtering;

namespace SakartveloSoft.API.Core.Tests
{
    class Obj : IEntityWithKey
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
                var propertyRef = Filters.Property<Obj, decimal>(obj => obj.Balance);
                Assert.AreEqual(propertyRef.NodeType, FilterNodeType.Property);
                var comparison = propertyRef > 4m;
                Assert.AreEqual(comparison.NodeType, FilterNodeType.Compare);
                var logicalOp = comparison && Filters.Property<Obj, string>(obj => obj.Name) == "Test";
                Assert.AreEqual(logicalOp.NodeType, FilterNodeType.BinaryLogicOp);
                var param = Filters.Parameter<string>("nameValue");
            }
            var builder = Filters.Builder<Obj>();

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

            var builder = Filters.Builder<Obj>();

            var param2 = builder.Parameter<string>("prop2");
            var builtFilter = builder.Build(builder.Property(a => a.Name) == param2 && builder.Property(a => a.Balance) > builder.Parameter<decimal>("prop3"), bag);

            Assert.IsTrue(builtFilter.HasParameter("prop2"), "Parameter prop3 not found in filter");
            Assert.IsTrue(builtFilter.HasParameter<string>("prop2"), "Parameter prop3 not found filter");
            Assert.AreEqual(builtFilter.GetParameterValue("prop3"), bag.prop3);
            Assert.AreEqual(builtFilter.GetParameter<string>("prop2"), bag.prop2);
            Assert.AreEqual(builtFilter.GetParameter(param2), bag.prop2);
        }

        [TestMethod]
        public void FindRequestParsingTest()
        {
            var request = DataSearchHelper.ParseSearchRequest<Obj>(@"
{
    ""filters"": {
        ""prop2"": ""Some text"", 
        ""prop3"": { ""$gte"": 2 }
    }
}");
            Assert.IsNull(request.Keywords);
            Assert.IsNotNull(request.Filter);
            Assert.AreNotEqual(request.Filter.Operation.NodeType, FilterNodeType.Unknown, "Node type must be well defined for all nodes");
            Assert.IsInstanceOfType(request.Filter.Operation, typeof(MultOperandsBooleanOperation));
        }

    }
}
