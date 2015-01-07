using System;

namespace Ektron.SharedSource.FluentApi.ModelAttributes
{
    /// <summary>
    /// Attribute indicating the name of the Smart Form field to find the value.
    /// </summary>
    public class SmartFormComplexAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartFormComplexAttribute"/> class.
        /// </summary>
        /// <param name="xpath">The xpath.</param>
        public SmartFormComplexAttribute(string xpath)
        {
            this.Xpath = xpath;
        }

        /// <summary>
        /// Gets the xpath the indicating the node with the complex type to be used.
        /// </summary>
        public string Xpath { get; private set; }
    }
}
