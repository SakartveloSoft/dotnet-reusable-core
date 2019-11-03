using Microsoft.VisualStudio.TestTools.UnitTesting;
using SakartveloSoft.API.DataAttributes;
using SakartveloSoft.API.Metadata;
using SakartveloSoft.API.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SakartveloSoft.API.Core.Tests
{
    [Persisted, StoreName("testEntities"), IdPrefix("to")]
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
        }
    }
}
