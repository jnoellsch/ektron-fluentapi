using System;
using System.Web.UI;
using Ektron.Cms.Content;
using Ektron.Cms.Framework.Content;
using Ektron.SharedSource.FluentApi;

namespace Ektron.SharedSource.Sandbox
{
    public partial class MappingTests : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = new ContentManager();

            var criteria = new ContentCriteria();

            var items = manager.GetList(criteria).AsContentType<Content>();

            this.rptrContent.DataSource = items;
            this.rptrContent.DataBind();
        }
    }
}