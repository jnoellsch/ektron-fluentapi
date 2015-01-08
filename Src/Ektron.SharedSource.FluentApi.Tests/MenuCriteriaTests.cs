namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Organization;
    using NUnit.Framework;
    using Ploeh.AutoFixture.NUnit2;

    public class MenuCriteriaTests
    {
        [TestFixture]
        public class ByMenuMethod
        {
            [Test, AutoData]
            public void SetsIdFilterIfLong(long id)
            {
                var sut = new MenuCriteria().ByMenu(id);
                
                Assert.AreEqual(1, sut.FilterGroups.Count);
                Assert.AreEqual(MenuProperty.Id, sut.FilterGroups[0].Filters[0].Field);
                Assert.AreEqual(CriteriaFilterOperator.EqualTo, sut.FilterGroups[0].Filters[0].Operator);
                Assert.AreEqual(id, sut.FilterGroups[0].Filters[0].Value);
            }

            [Test, AutoData]
            public void SetsTextFilterIfName(string name)
            {
                var sut = new MenuCriteria().ByMenu(name);

                Assert.AreEqual(1, sut.FilterGroups.Count);
                Assert.AreEqual(MenuProperty.Text, sut.FilterGroups[0].Filters[0].Field);
                Assert.AreEqual(CriteriaFilterOperator.EqualTo, sut.FilterGroups[0].Filters[0].Operator);
                Assert.AreEqual(name, sut.FilterGroups[0].Filters[0].Value);
            }
        }
    }
}
