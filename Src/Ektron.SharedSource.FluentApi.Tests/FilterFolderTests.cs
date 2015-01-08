namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms;
    using Ektron.Cms.Common;
    using NUnit.Framework;
    using Ploeh.AutoFixture.NUnit2;

    public class FilterFolderTests
    {
        [TestFixture]
        public class Constructor : TestBase
        {
            [Test]
            public void HasGuardClauses()
            {
                this.GuardClauseAssertion.Verify(typeof(FilterFolder).GetConstructors());
            }

            [Test]
            public void SetsProperties()
            {
                this.ConstructorInitializedMemberAssertion.Verify(typeof(FilterFolder).GetConstructors());
            }
        }

        [TestFixture]
        public class AddFilterMethod
        {
            [Test, AutoData]
            public void AddsToCriteriaFilter(string value)
            {
                var field = FolderProperty.FolderPath;
                var criteria = new FolderCriteria();
                var sut = new FilterFolder(criteria, field);

                sut.Contains(value);

                Assert.AreEqual(1, criteria.FilterGroups.Count);
                Assert.AreEqual(field, criteria.FilterGroups[0].Filters[0].Field);
                Assert.AreEqual(value, criteria.FilterGroups[0].Filters[0].Value);
            }
        }
    }
}
