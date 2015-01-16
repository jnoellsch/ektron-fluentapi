namespace Ektron.SharedSource.Sandbox.Models
{
    using Ektron.SharedSource.FluentApi.Mapping.Attributes;

    public class SearchTestModel
    {
        [SmartFormPrimitive("Color1")]
        public string Color1 { get; set; }

        [SmartFormPrimitive("Color2")]
        public string Color2 { get; set; }
    }
}