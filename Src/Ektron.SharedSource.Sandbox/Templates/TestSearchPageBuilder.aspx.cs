namespace Ektron.SharedSource.Sandbox.Templates
{
    using System;

    using Ektron.Cms.PageBuilder;

    public partial class TestSearchPageBuilder : PageBuilder
    {
        public override void Error(string message)
        {
            return;
        }

        public override void Notify(string message)
        {
            return;
        }
    }
}