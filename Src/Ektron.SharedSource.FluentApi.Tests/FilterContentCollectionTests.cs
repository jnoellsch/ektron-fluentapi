namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    using NUnit.Framework;

    using Ploeh.AutoFixture.NUnit2;

    public class FilterContentCollectionTests
    {
        [TestFixture]
        public class Constructor : TestBase
        {
            [Test]
            public void HasGuardClauses()
            {
                this.GuardClauseAssertion.Verify(typeof(FilterContentCollection).GetConstructors());
            }
        }

        [TestFixture]
        public class AddFilterMethod
        {
            [Test, AutoData]
            public void AddsToCriteriaFilter(string value)
            {
                var field = ContentCollectionProperty.Description;
                var criteria = new ContentCollectionCriteria();
                var sut = new FilterContentCollection(criteria, field);

                sut.Contains(value);

                Assert.AreEqual(1, criteria.CollectionGroupFilters.Count);
                Assert.AreEqual(field, criteria.CollectionGroupFilters[0].Filters[0].Field);
                Assert.AreEqual(value, criteria.CollectionGroupFilters[0].Filters[0].Value);
            }
        }
    }
}
