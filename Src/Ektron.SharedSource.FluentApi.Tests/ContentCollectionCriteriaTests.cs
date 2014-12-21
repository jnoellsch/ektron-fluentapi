namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;
    using NUnit.Framework;

    public class ContentCollectionCriteriaTests
    {
        public class IntegrationValidation
        {
            public void Exercise()
            {
                var sut = new ContentCollectionCriteria()
                    .ByCollection(10)
                    .FilteredBy(ContentCollectionProperty.Description).Contains(10)
                    .Ordered();
            }    
        }

        [TestFixture]
        public class ByCollectionMethod
        {
            [TestCase(5)]
            public void SetsFilterIfInteger(int id)
            {
                var sut = new ContentCollectionCriteria().ByCollection(id);
                
                Assert.AreEqual(1, sut.CollectionGroupFilters.Count);
                Assert.AreEqual(id, sut.CollectionGroupFilters[0].Filters[0].Value);
            }

            [TestCase("Test Name")]
            public void SetsFilterIfName(string name)
            {
                var sut = new ContentCollectionCriteria().ByCollection(name);

                Assert.AreEqual(1, sut.CollectionGroupFilters.Count);
                Assert.AreEqual(name, sut.CollectionGroupFilters[0].Filters[0].Value);
            }
        }

        [TestFixture]
        public class OrderedMethod
        {
            [Test]
            public void SetsProperty()
            {
                var sut = new ContentCollectionCriteria().Ordered();

                Assert.AreEqual(true, sut.OrderByCollectionOrder);
            }
        }
    }
}
