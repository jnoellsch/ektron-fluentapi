namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.User;
    using NUnit.Framework;

    public class UserCriteriaTests
    {
        [TestFixture]
        public class WithCustomPropertiesMethod
        {
            [Test]
            public void SetsReturnCustomPropertiesToTrue()
            {
                var sut = new UserCriteria().WithCustomProperties();
                Assert.IsTrue(sut.ReturnCustomProperties);
            }
        }
    }
}
