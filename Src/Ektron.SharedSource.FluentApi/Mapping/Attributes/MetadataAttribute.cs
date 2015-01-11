using System;

namespace Ektron.SharedSource.FluentApi.Mapping.Attributes
{
    /// <summary>
    /// Attribute indicating the name of the metadata field to find the value.
    /// </summary>
    public class MetadataAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataAttribute"/> class.
        /// </summary>
        /// <param name="fieldName">The name of the metadata field.</param>
        public MetadataAttribute(string fieldName)
        {
            this.FieldName = fieldName;
        }

        /// <summary>
        /// Gets the name of the metadata field.
        /// </summary>
        public string FieldName { get; private set; }
    }
}
