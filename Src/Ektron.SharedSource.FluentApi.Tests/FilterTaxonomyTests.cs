using Ektron.Cms.Organization;

namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    using NUnit.Framework;

    using Ploeh.AutoFixture.NUnit2;

    public class FilterTaxonomyTests
    {
        [TestFixture]
        public class Constructor : TestBase
        {
            [Test]
            public void HasGuardClauses()
            {
                this.GuardClauseAssertion.Verify(typeof(FilterTaxonomy).GetConstructors());
            }

            [Test]
            public void SetsProperties()
            {
                this.ConstructorInitializedMemberAssertion.Verify(typeof(FilterTaxonomy).GetConstructors());
            }
        }

        [TestFixture]
        public class AddFilterMethod
        {
            [Test, AutoData]
            public void AddsToCriteriaFilter(string value)
            {
                var field = TaxonomyProperty.Path;
                var criteria = new TaxonomyCriteria();
                var sut = new FilterTaxonomy(criteria, field);

                sut.Contains(value);

                Assert.AreEqual(1, criteria.FilterGroups.Count);
                Assert.AreEqual(field, criteria.FilterGroups[0].Filters[0].Field);
                Assert.AreEqual(value, criteria.FilterGroups[0].Filters[0].Value);
            }
        }
    }
}
