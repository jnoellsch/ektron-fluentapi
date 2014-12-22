namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;
    using NUnit.Framework;

    using Ploeh.AutoFixture.NUnit2;

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
            [Test, AutoData]
            public void SetsFilterIfLong(long id)
            {
                var sut = new ContentCollectionCriteria().ByCollection(id);
                
                Assert.AreEqual(1, sut.CollectionGroupFilters.Count);
                Assert.AreEqual(ContentCollectionProperty.Id, sut.CollectionGroupFilters[0].Filters[0].Field);
                Assert.AreEqual(id, sut.CollectionGroupFilters[0].Filters[0].Value);
            }

            [Test, AutoData]
            public void SetsFilterIfName(string name)
            {
                var sut = new ContentCollectionCriteria().ByCollection(name);

                Assert.AreEqual(1, sut.CollectionGroupFilters.Count);
                Assert.AreEqual(ContentCollectionProperty.Title, sut.CollectionGroupFilters[0].Filters[0].Field);
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

                Assert.True(sut.OrderByCollectionOrder);
            }
        }
    }
}
