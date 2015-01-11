using System;

namespace Ektron.SharedSource.FluentApi.Mapping.Attributes
{
    /// <summary>
    /// Attribute indicating the name of the Smart Form field to find the value.
    /// </summary>
    public class SmartFormPrimitiveAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFormPrimitiveAttribute"/> class.
        /// </summary>
        /// <param name="xpath">The xpath.</param>
        public SmartFormPrimitiveAttribute(string xpath)
        {
            this.Xpath = xpath;
        }

        /// <summary>
        /// Gets the xpath the indicating the node with the value to be used.
        /// </summary>
        public string Xpath { get; private set; }
    }
}
