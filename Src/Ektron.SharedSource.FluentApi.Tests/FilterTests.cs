namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ektron.Cms.Common;

    using Moq;
    using Moq.Protected;

    using NUnit.Framework;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Idioms;
    using Ploeh.AutoFixture.NUnit2;

    public class FilterTests
    {
        [TestFixture]
        public class ContainsMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.Contains(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.Contains, value);
            }
        }

        [TestFixture]
        public class DoesNotContainMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.DoesNotContain(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.DoesNotContain, value);
            }
        }

        [TestFixture]
        public class EndsWithMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.EndsWith(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.EndsWith, value);
            }
        }

        [TestFixture]
        public class EqualToMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.EqualTo(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.EqualTo, value);
            }
        }

        [TestFixture]
        public class GreaterThanMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.GreaterThan(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.GreaterThan, value);
            }
        }

        [TestFixture]
        public class GreaterThanOrEqualToMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.GreaterThanOrEqualTo(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.GreaterThanOrEqualTo, value);
            }
        }

        [TestFixture]
        public class InMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.In(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.In, value);
            }
        }

        [TestFixture]
        public class InSubClauseMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.InSubClause(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.InSubClause, value);
            }
        }

        [TestFixture]
        public class IsNullMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.IsNull(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.IsNull, value);
            }
        }

        [TestFixture]
        public class LessThanMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.LessThan(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.LessThan, value);
            }
        }

        [TestFixture]
        public class LessThanOrEqualToMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.LessThanOrEqualTo(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.LessThanOrEqualTo, value);
            }
        }

        [TestFixture]
        public class NotEqualToMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.NotEqualTo(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.NotEqualTo, value);
            }
        }

        [TestFixture]
        public class NotInMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.NotIn(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.NotIn, value);
            }
        }

        [TestFixture]
        public class StartsWithMethod
        {
            [Test, AutoData]
            public void CallsAddFilter(string value)
            {
                var sut = new Mock<Filter<object>>();
                sut.Object.StartsWith(value);
                sut.Protected().Verify("AddFilter", Times.Once(), CriteriaFilterOperator.StartsWith, value);
            }
        }
    }
}
