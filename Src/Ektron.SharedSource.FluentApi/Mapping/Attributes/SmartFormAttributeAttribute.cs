using System;

namespace Ektron.SharedSource.FluentApi.Mapping.Attributes
{
    /// <summary>
    /// Attribute indicating the name of the Smart Form attribute to find the value.
    /// </summary>
    public class SmartFormAttributeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFormAttributeAttribute"/> class.
        /// </summary>
        /// <param name="xpath">The xpath.</param>
        public SmartFormAttributeAttribute(string xpath)
        {
            this.Xpath = xpath;
        }

        /// <summary>
        /// Gets the xpath the indicating the attribute with the value to be used.
        /// </summary>
        public string Xpath { get; private set; }
    }
}
