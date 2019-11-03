using Microsoft.VisualStudio.TestTools.UnitTesting;
using SakartveloSoft.API.DataAttributes;
using SakartveloSoft.API.Metadata;
using SakartveloSoft.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace SakartveloSoft.API.Core.Tests
{
    [KnownType, Persisted, StoreName("testEntities"), IdPrefix("to")]
    public class TestObj
    {
        [Required, EntityKey(EntityKeyType.PrefixedCompactRandomString)]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, InRange(min:0)]
        public int Balance { get; set; }
        [DefaultValue(true)]
        public bool Active { get; set; }
    }

    [TestClass]
    public class TestMetadataAndAttributes
    {
        [TestMethod]
        public void TestMetaTypeAttributes()
        {
            var metadata = new MetadataManager();
            metadata.DiscoverType<TestObj>();
            var newObj = metadata.CreateNewObject<TestObj>();
            Assert.AreEqual(newObj.Active, true);
            Assert.IsTrue(newObj.Id != null);
        }

        [TestMethod]
        public void TestTypesAutoDiscovery()
        {
            var metadataManager = new MetadataManager();
            metadataManager.DiscoverAssembly(this.GetType().Assembly);
            Assert.IsTrue(metadataManager.HasMetadataForType<TestObj>(), "Type information was not added with autodiscovery by assembly");
            var newObj = metadataManager.CreateNewObject<TestObj>();
            Assert.IsNotNull(newObj.Id);
            Assert.IsTrue(newObj.Id.StartsWith("to_"), "Incorrect oject id was generated");
            Console.WriteLine($@"Generated id {newObj.Id} for tests");
        }

        [TestMethod]
        public void TestValidationFlow()
        {
            var metadataManager = new MetadataManager();
            metadataManager.DiscoverAssembly(this.GetType().Assembly);
            Assert.IsTrue(metadataManager.HasMetadataForType<TestObj>(), "Type information was not added with autodiscovery by assembly");
            var newObj = metadataManager.CreateNewObject<TestObj>();
            var report = metadataManager.ValidateObject<TestObj>(newObj);
            Assert.IsFalse(report.IsValid, "Validation report for new blank test object must be invalid");
            newObj.Name = "";
            report = metadataManager.ValidateObject(newObj);
            Assert.IsFalse(report.IsValid, "Validation report for new blank test object must be invalid with blank name");
            newObj.Name = "Test Thingy";
            report = metadataManager.ValidateObject(newObj);
            Assert.IsTrue(report.IsValid, "Validation report for updated test object must be valid with name set");

        }
    }

    
}
