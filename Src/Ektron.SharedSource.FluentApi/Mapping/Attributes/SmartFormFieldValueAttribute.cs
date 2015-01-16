using System;

namespace Ektron.SharedSource.FluentApi.Mapping.Attributes
{
    /// <summary>
    /// Attribute indicating the name of the Smart Form field to find the value.
    /// </summary>
    public class SmartFormFieldValueAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFormFieldValueAttribute"/> class.
        /// </summary>
        /// <param name="xpath">The xpath.</param>
        public SmartFormFieldValueAttribute(string xpath)
        {
            this.Xpath = xpath;
        }

        /// <summary>
        /// Gets the xpath the indicating the node with the value to be used.
        /// </summary>
        public string Xpath { get; private set; }
    }
}
