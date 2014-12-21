namespace Ektron.SharedSource.FluentApi.Tests
{
    using NUnit.Core.Extensibility;

    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Idioms;

    [NUnitAddin]
    public class LocalAddin : Ploeh.AutoFixture.NUnit2.Addins.Addin
    {
        public IFixture Fixture { get; set; }

        public GuardClauseAssertion GuardClauseAssertion { get; set; }

        public WritablePropertyAssertion WritablePropertyAssertion { get; set; }

        protected void Setup()
        {
            this.Fixture = new Fixture().Customize(new AutoMoqCustomization());
            this.GuardClauseAssertion = new GuardClauseAssertion(this.Fixture);
            this.WritablePropertyAssertion = new WritablePropertyAssertion(this.Fixture);
        }
    }
}