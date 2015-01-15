using System;
using System.Web.UI;
using Ektron.Cms.Content;
using Ektron.Cms.Framework.Content;
using Ektron.SharedSource.FluentApi;
using Ektron.SharedSource.FluentApi.Mapping;

namespace Ektron.SharedSource.Sandbox
{
    public partial class MappingTests : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = new ContentManager();

            var criteria = new ContentCriteria();

            Mapper.RegisterMapping<Content>((contentData, t) =>
            {
                t.Title = t.Title.ToUpper();
            });

            var items = manager.GetList(criteria).AsContentType<Content>();

            this.rptrContent.DataSource = items;
            this.rptrContent.DataBind();
        }
    }
}