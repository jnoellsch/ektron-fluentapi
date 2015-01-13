using Ektron.Cms;
using Ektron.Cms.Organization;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests
{
    public class TaxonomyCriteriaTests
    {
        [TestFixture]
        public class RecursiveMethod
        {
            [Test]
            public void SetsReturnRecursiveChildrenPropertyToTrue()
            {
                var sut = new TaxonomyCriteria().Recursive();
                Assert.IsTrue(sut.ReturnRecursiveChildren);
            }
        }
    }
}
