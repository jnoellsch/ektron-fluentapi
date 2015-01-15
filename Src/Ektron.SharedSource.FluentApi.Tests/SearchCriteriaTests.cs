namespace Ektron.SharedSource.FluentApi.Tests
{
    using System.Linq;

    using Ektron.Cms.Search;

    using NUnit.Framework;

    public class SearchCriteriaTests
    {
        [TestFixture]
        public class EnableRefinementMethod
        {
            [Test]
            public void SetsEnableRefinementPropertyToTrue()
            {
                var sut = new KeywordSearchCriteria().EnableRefinement();
                Assert.IsTrue(sut.Refinement.IsEnabled);
            }
        }

        [TestFixture]
        public class RefineByMethod
        {
            [Test]
            public void AddsPropertyAsRefinement()
            {
                var property = SearchContentProperty.Title;
                var criteria = new KeywordSearchCriteria()
                    .RefineBy(SearchContentProperty.Title);
                var firstRefinement = criteria.Refinement.Items.FirstOrDefault();
                Assert.IsNotNull(firstRefinement);
                Assert.AreEqual(
                    firstRefinement.Property, 
                    property);
            }
        }

        [TestFixture]
        public class RefineMultiValueByMethod
        {
        }

        [TestFixture]
        public class ReturnsWithMethod
        {
        }
    }
}