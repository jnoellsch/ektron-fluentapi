namespace Ektron.SharedSource.Sandbox.Templates
{
    using System;
    using System.Web;

    using Ektron.Cms.Framework.Content;
    using Ektron.SharedSource.FluentApi;
    using Ektron.SharedSource.Sandbox.Models;

    public partial class TestSearchSmartForm : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var manager = new ContentManager();

            long id;
            id = long.TryParse(Convert.ToString(this.Request.QueryString["id"]), out id) ? id : -1;

            var model = manager.GetItem(id).AsContentType<SearchTestModel>();

            this.litColor1.Text = model.Color1;
            this.litColor2.Text = model.Color2;
        }
    }
}