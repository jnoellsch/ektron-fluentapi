using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    /// <summary>
    /// Provides a mapping for metadata on <see cref="ContentData"/> onto an object.
    /// </summary>
    internal static class MetadataMapper
    {
        /// <summary>
        /// Gets a mapping for properties from metadata fields on <see cref="ContentData"/> to an instance of T.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <returns>An <see cref="Action"/> that maps metadata fields onto an instance of type T.</returns>
        public static Action<ContentData, T> GetMapping<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var propertyMappings = new List<Action<ContentData, T>>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<MetadataAttribute>();
                if (attribute == null) continue;
                if (string.IsNullOrWhiteSpace(attribute.FieldName)) continue;

                if (StringMapper.IsMappable(propertyInfo.PropertyType))
                {
                    propertyMappings.Add(GetPrimitiveMapping<T>(propertyInfo, attribute.FieldName));
                }
                else if (StringMapper.IsMappableEnumerable(propertyInfo.PropertyType))
                {
                    propertyMappings.Add(GetEnumerableMapping<T>(propertyInfo, attribute.FieldName));
                }
            }

            return (contentData, t) => propertyMappings.ForEach(mapping => mapping(contentData, t));
        }

        /// <summary>
        /// Gets a mapping from a metadata field on <see cref="ContentData"/> to a property on an instance of T.
        /// </summary>
        /// <typeparam name="T">The type that is being mapped to.</typeparam>
        /// <param name="propertyInfo">The property on T.</param>
        /// <param name="fieldName">The name of the metadata field to map.</param>
        /// <returns>An <see cref="Action"/> that maps a single metadata field on <see cref="ContentData"/> to a single property on an instance of T.</returns>
        private static Action<ContentData, T> GetPrimitiveMapping<T>(PropertyInfo propertyInfo, string fieldName) where T : new()
        {
            var mapToPropertyType = StringMapper.GetMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (contentData, t) =>
            {
                var metadata = GetMetadata(contentData, fieldName);
                var value = mapToPropertyType(metadata.Text);

                setProperty(t, value);
            };
        }

        /// <summary>
        /// Gets a mapping from a metadata field with multiple values on <see cref="ContentData"/> to a property on an instance of T.
        /// </summary>
        /// <typeparam name="T">The type that is being mapped to.</typeparam>
        /// <param name="propertyInfo">The property on T.</param>
        /// <param name="fieldName">The name of the metadata field with multiple values to map.</param>
        /// <returns>An <see cref="Action"/> that maps a single metadata field on <see cref="ContentData"/> to a single property on an instance of T.</returns>
        private static Action<ContentData, T> GetEnumerableMapping<T>(PropertyInfo propertyInfo, string fieldName) where T : new()
        {
            var mapToPropertyType = StringMapper.GetEnumerableMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (contentData, t) =>
            {
                var metadata = GetMetadata(contentData, fieldName);
                var rawValues = metadata.Text.Split(metadata.Separator[0]);
                var values = mapToPropertyType(rawValues);

                setProperty(t, values);
            };
        }

        /// <summary>
        /// Gets the <see cref="ContentMetaData"/> field based on the field name.
        /// </summary>
        /// <param name="contentData">The <see cref="ContentData"/> to get the metadata from.</param>
        /// <param name="fieldName">The name of the metadata field.</param>
        /// <returns>The metadata field.</returns>
        private static ContentMetaData GetMetadata(ContentData contentData, string fieldName)
        {
            var metadata = contentData.MetaData.SingleOrDefault(x => x.Name == fieldName);
            if (metadata == null) throw new Exception("Metadata field does not exist.");

            return metadata;
        }
    }
}
