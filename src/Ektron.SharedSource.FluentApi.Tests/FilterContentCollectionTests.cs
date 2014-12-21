namespace Ektron.SharedSource.FluentApi.Tests
{
    using NUnit.Framework;

    public class FilterContentCollectionTests
    {
        [TestFixture]
        public class Constructor : TestBase
        {
            [Test]
            public void HasGuardClauses()
            {
                this.GuardClauseAssertion.Verify(typeof(FilterContentCollection).GetConstructors());
            }
        }
    }
}
