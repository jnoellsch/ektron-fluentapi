using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    internal static class MetadataMapper
    {
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
                    propertyMappings.Add(GetPrimitiveMapping<T>(propertyInfo, attribute));
                }
                else if (StringMapper.IsMappableEnumerable(propertyInfo.PropertyType))
                {
                    propertyMappings.Add(GetEnumerableMapping<T>(propertyInfo, attribute));
                }
            }

            return (contentData, t) => propertyMappings.ForEach(mapping => mapping(contentData, t));
        }

        private static Action<ContentData, T> GetPrimitiveMapping<T>(PropertyInfo propertyInfo, MetadataAttribute attribute) where T : new()
        {
            var mapToPropertyType = StringMapper.GetMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (contentData, t) =>
            {
                var metadata = GetMetadata(contentData, attribute.FieldName);
                var value = mapToPropertyType(metadata.Text);

                setProperty(t, value);
            };
        }

        private static Action<ContentData, T> GetEnumerableMapping<T>(PropertyInfo propertyInfo, MetadataAttribute attribute) where T : new()
        {
            var mapToPropertyType = StringMapper.GetEnumerableMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (contentData, t) =>
            {
                var metadata = GetMetadata(contentData, attribute.FieldName);
                var rawValues = metadata.Text.Split(metadata.Separator[0]);
                var values = mapToPropertyType(rawValues);

                setProperty(t, values);
            };
        }

        private static ContentMetaData GetMetadata(ContentData contentData, string fieldName)
        {
            var metadata = contentData.MetaData.SingleOrDefault(x => x.Name == fieldName);
            if (metadata == null) throw new Exception("Metadata field does not exist.");

            return metadata;
        }
    }
}
