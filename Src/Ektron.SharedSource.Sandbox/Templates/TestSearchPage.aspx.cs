namespace Ektron.SharedSource.Sandbox.Templates
{
    using System;

    using Ektron.Cms.Framework.Search;
    using Ektron.Cms.PageBuilder;
    using Ektron.Cms.Search;
    using Ektron.SharedSource.FluentApi;

    public partial class TestSearchPage : PageBuilder
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.btnSearch.Click += (sender, args) => this.Search();
        }

        private void Search()
        {
            string query = this.txtQuery.Text;
            var searchManager = new SearchManager();
            var criteria = new KeywordSearchCriteria();
            criteria.QueryText = query;
            criteria.OrderBy.Add(new OrderData(SearchContentProperty.Title, OrderDirection.Ascending));
            criteria.ReturnsWith(
                SearchContentProperty.HighlightedSummary,
                SearchContentProperty.Id,
                SearchContentProperty.QuickLink,
                SearchContentProperty.Title);
            var searchResult = searchManager.Search(criteria);
            this.lvResults.DataSource = searchResult.Results;
            this.lvResults.DataBind();
            this.txtQuery.Focus();
        }

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