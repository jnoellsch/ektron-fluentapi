namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    using NUnit.Framework;

    using Ploeh.AutoFixture.NUnit2;

    public class ContentCriteriaTests
    {
        [TestFixture]
        public class OrSeparatedMethod
        {
            [Test]
            public void SetsConditionPropertyToOr()
            {
                var sut = new ContentCriteria().OrSeparated();
                Assert.AreEqual(LogicalOperation.Or, sut.Condition);
            }
        }

        [TestFixture]
        public class AndSeparatedMethod
        {
            [Test]
            public void SetsConditionPropertyToAnd()
            {
                var sut = new ContentCriteria().OrSeparated().AndSeparated();
                Assert.AreEqual(LogicalOperation.And, sut.Condition);
            }
        }

        [TestFixture]
        public class RecursiveMethod
        {
            [Test]
            public void SetsFolderRecursivePropertyToTrue()
            {
                var sut = new ContentCriteria().Recursive();
                Assert.IsTrue(sut.FolderRecursive);
            }
        }

        [TestFixture]
        public class MaxItemsMethod
        {
            [Test]
            public void SetsPagingPropertyToIntMax()
            {
                var sut = new ContentCriteria().MaxItems();
                Assert.AreEqual(int.MaxValue, sut.PagingInfo.RecordsPerPage);
            }
        }

        [TestFixture]
        public class WithMetadataMethod
        {
            [Test]
            public void SetsReturnMetadataPropertyToTrue()
            {
                var sut = new ContentCriteria().WithMetadata();
                Assert.IsTrue(sut.ReturnMetadata);
            }
        }

        [TestFixture]
        public class BySmartFormMethod
        {
            [Test, AutoData]
            public void AddsFilterForXmlConfig(int id)
            {
                var sut = new ContentCriteria().BySmartForm(id);
                
                Assert.AreEqual(1, sut.FilterGroups.Count);
                Assert.AreEqual(ContentProperty.XmlConfigurationId, sut.FilterGroups[0].Filters[0].Field);
                Assert.AreEqual(id, sut.FilterGroups[0].Filters[0].Value);
            }
        }

        [TestFixture]
        public class OrderByMethod
        {
            [TestCase(ContentProperty.Title)]
            [TestCase(ContentProperty.Path)]
            public void SetsOrderFieldAndAscendingDirection(ContentProperty field)
            {
                var sut = new ContentCriteria().OrderBy(field);

                Assert.AreEqual(EkEnumeration.OrderByDirection.Ascending, sut.OrderByDirection);
                Assert.AreEqual(field, sut.OrderByField);
            }
        }

        [TestFixture]
        public class OrderByDescendingMethod
        {
            [TestCase(ContentProperty.Title)]
            [TestCase(ContentProperty.Path)]
            public void SetsOrderFieldAndDescendingDirection(ContentProperty field)
            {
                var sut = new ContentCriteria().OrderByDescending(field);

                Assert.AreEqual(EkEnumeration.OrderByDirection.Descending, sut.OrderByDirection);
                Assert.AreEqual(field, sut.OrderByField);
            }   
        }
    }
}
