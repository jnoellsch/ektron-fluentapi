using System;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mapping.Attributes
{
    /// <summary>
    /// Attribute indicating the name of the property on the <see cref="ContentData"/> to find the value.
    /// </summary>
    public class ContentDataAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContentDataAttribute"/> class.
        /// </summary>
        /// <param name="propertyName">The name of the property on the <see cref="ContentData"/>.</param>
        public ContentDataAttribute(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        public string PropertyName { get; private set; }
    }
}
