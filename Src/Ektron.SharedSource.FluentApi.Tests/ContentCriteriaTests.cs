﻿namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    using NUnit.Framework;

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
    }
}
