using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi
{
    /// <summary>
    /// Represents a set of extensions for <see cref="ContentData"/>.
    /// </summary>
    public static class ContentDataExtensions
    {
        /// <summary>
        /// Converts a set of ContentData into some content type using Ektron deserialization.
        /// </summary>
        /// <typeparam name="T">Content Type to convert <see cref="ContentData"/> to.</typeparam>
        /// <param name="source">A set of <see cref="ContentData"/> to convert.</param>
        /// <returns>A set of content types.</returns>
        public static IEnumerable<T> AsContentType<T>(this IEnumerable<ContentData> source)
        {
            return source.Select(AsContentType<T>);
        }

        /// <summary>
        /// Converts a ContentData into some Content Type using Ektron deserialization.
        /// </summary>
        /// <typeparam name="T">Content Type to convert <see cref="ContentData"/> to.</typeparam>
        /// <param name="source">The <see cref="ContentData"/> to convert.</param>
        /// <returns>A content type.</returns>
        public static T AsContentType<T>(this ContentData source)
        {
            var properties = typeof(T).GetProperties();
            var result = Activator.CreateInstance<T>();

            foreach (var property in properties)
            {
                try
                {
                    var xml = XDocument.Parse(source.Html);
                    if (TryApplySmartFormValue(source, property, result, xml)) continue;
                }
                catch
                {
                }

                if (TryApplyContentDataValue(source, property, result)) continue;

                if (TryApplyMetadataValue(source, property, result)) continue;
            }

            return result;
        }

        /// <summary>
        /// Attempts to use attribute to apply smart form value.
        /// </summary>
        /// <typeparam name="T">Type of result.</typeparam>
        /// <param name="source"><see cref="ContentData"/> source.</param>
        /// <param name="property">The property to be set.</param>
        /// <param name="result">The object whose property will be set.</param>
        /// <returns>A boolean indicating the success or failure of the action.</returns>
        private static bool TryApplyMetadataValue<T>(ContentData source, PropertyInfo property, T result)
        {
            var metadataProperty = property.GetCustomAttributes<MetadataAttribute>().SingleOrDefault();

            if (metadataProperty == null) return false;
            
            var metadata = source.MetaData.SingleOrDefault(x => x.Name == metadataProperty.FieldName);

            if (metadata == null) return false;
            
            property.SetValue(result, metadata.Text);
            
            return true;
        }

        /// <summary>
        /// Attempts to use attribute to apply <see cref="ContentData"/> value.
        /// </summary>
        /// <typeparam name="T">Type of result.</typeparam>
        /// <param name="source"><see cref="ContentData"/> source.</param>
        /// <param name="property">The property to be set.</param>
        /// <param name="result">The object whose property will be set.</param>
        /// <returns>A boolean indicating the success or failure of the action.</returns>
        private static bool TryApplyContentDataValue<T>(ContentData source, PropertyInfo property, T result)
        {
            var contentDataProperty = property.GetCustomAttributes<ContentDataAttribute>().SingleOrDefault();

            if (contentDataProperty == null) return false;

            var sourceProperty = typeof(ContentData).GetProperty(contentDataProperty.PropertyName);

            if (sourceProperty == null) return false;
            
            var value = sourceProperty.GetValue(source);
            property.SetValue(result, value);
            
            return true;
        }

        /// <summary>
        /// Attempts to use attribute to apply metadata value.
        /// </summary>
        /// <typeparam name="T">Type of result.</typeparam>
        /// <param name="source"><see cref="ContentData"/> source.</param>
        /// <param name="property">The property to be set.</param>
        /// <param name="result">The object whose property will be set.</param>
        /// <param name="xml">The <see cref="XDocument"/> of the Html source.</param>
        /// <returns>A boolean indicating the success or failure of the action.</returns>
        private static bool TryApplySmartFormValue<T>(ContentData source, PropertyInfo property, T result, XDocument xml)
        {
            var smartformProperty = property.GetCustomAttributes<SmartFormAttribute>().SingleOrDefault();

            if (smartformProperty == null) return false;

            var node = xml.XPathSelectElement(smartformProperty.Xpath);

            if (node == null) return false;
            
            property.SetValue(result, node.Value);
            
            return true;
        }
    }
}
