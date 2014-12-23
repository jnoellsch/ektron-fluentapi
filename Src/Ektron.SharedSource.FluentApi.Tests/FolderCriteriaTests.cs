namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms;
    using Ektron.Cms.Common;

    using NUnit.Framework;

    using Ploeh.AutoFixture.NUnit2;

    public class FolderCriteriaTests
    {
        [TestFixture]
        public class ByFolderMethod
        {
            [Test, AutoData]
            public void SetsIdFilterIfLong(long id)
            {
                var sut = new FolderCriteria().ByFolder(id);

                Assert.AreEqual(1, sut.FilterGroups.Count);
                Assert.AreEqual(FolderProperty.Id, sut.FilterGroups[0].Filters[0].Field);
                Assert.AreEqual(CriteriaFilterOperator.EqualTo, sut.FilterGroups[0].Filters[0].Operator);
                Assert.AreEqual(id, sut.FilterGroups[0].Filters[0].Value);
            }

            [Test, AutoData]
            public void SetsPathFilterIfString(string path)
            {
                var sut = new FolderCriteria().ByFolder(path);

                Assert.AreEqual(1, sut.FilterGroups.Count);
                Assert.AreEqual(FolderProperty.FolderPath, sut.FilterGroups[0].Filters[0].Field);
                Assert.AreEqual(CriteriaFilterOperator.EqualTo, sut.FilterGroups[0].Filters[0].Operator);
                Assert.AreEqual(path, sut.FilterGroups[0].Filters[0].Value);
            }
        }

        [TestFixture]
        public class WithChildPropertiesMethod
        {
            [Test]
            public void SetsWithChildPropertiesPropertyToTrue()
            {
                var sut = new FolderCriteria().WithChildProperties();
                Assert.IsTrue(sut.ReturnChildProperties);
            }
        }
    }
}
