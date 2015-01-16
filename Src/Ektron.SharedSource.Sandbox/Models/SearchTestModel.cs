namespace Ektron.SharedSource.Sandbox.Models
{
    using System.Collections.Generic;

    using Ektron.SharedSource.FluentApi.Mapping.Attributes;

    public class SearchTestModel
    {
        [SmartFormFieldValueAttribute("Color1")]
        public string Color1 { get; set; }

        [SmartFormFieldValueAttribute("Color2")]
        public string Color2 { get; set; }

        [SmartFormObject("SecondaryColors")]
        public IEnumerable<SecondaryColor> SecondaryColors { get; set; }

        public class SecondaryColor
        {
            [SmartFormFieldValueAttribute("ColorName")]
            public string ColorName { get; set; }

            [SmartFormFieldValueAttribute("TestCheckbox")]
            public bool TestCheckbox { get; set; }
        }
    }
}