namespace Ektron.SharedSource.Sandbox.Templates
{
    using System;

    using Ektron.Cms.Framework.Content;

    public partial class TestSearchHtmlContent : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var manager = new ContentManager();

            long id;
            id = long.TryParse(Convert.ToString(this.Request.QueryString["id"]), out id) ? id : -1;

            var model = manager.GetItem(id);

            this.litContent.Text = model.Html;
        }
    }
}