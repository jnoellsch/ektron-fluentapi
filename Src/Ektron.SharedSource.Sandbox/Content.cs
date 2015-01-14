using Ektron.SharedSource.FluentApi.Mapping.Attributes;

namespace Ektron.SharedSource.Sandbox
{
    public class Content
    {
        [ContentData("Title")]
        public string Title { get; set; }
    }
}