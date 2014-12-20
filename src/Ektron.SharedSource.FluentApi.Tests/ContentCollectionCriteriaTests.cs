namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Content;
    using NUnit.Framework;

    public class ContentCollectionCriteriaTests
    {
        public class ByCollectionMethod
        {
            [Test]
            public void SetsFilterIfInteger()
            {
                var sut = new ContentCollectionCriteria().ByCollection(5);
                
                Assert.AreEqual(1, sut.CollectionGroupFilters.Count);
                Assert.AreEqual(5, sut.CollectionGroupFilters[0].Filters[0].Value);
            }

            [Test]
            public void SkipsFilterIfNull()
            {
                var sut = new ContentCollectionCriteria().ByCollection(null);
                Assert.AreEqual(0, sut.CollectionGroupFilters.Count);
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
