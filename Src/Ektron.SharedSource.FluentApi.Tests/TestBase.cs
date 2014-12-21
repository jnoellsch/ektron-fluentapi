namespace Ektron.SharedSource.FluentApi.Tests
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Idioms;

    public class TestBase
    {
        protected TestBase()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
            this.GuardClauseAssertion = new GuardClauseAssertion(this.Fixture);
            this.WritablePropertyAssertion = new WritablePropertyAssertion(this.Fixture);
        }

        public IFixture Fixture { get; set; }

        public GuardClauseAssertion GuardClauseAssertion { get; set; }

        public WritablePropertyAssertion WritablePropertyAssertion { get; set; }
    }
}
