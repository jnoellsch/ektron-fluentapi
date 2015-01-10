using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi.Mappers
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
            var setter = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (contentData, t) =>
            {
                var metadata = contentData.MetaData.SingleOrDefault(x => x.Name == attribute.FieldName);
                if (metadata == null) return;

                var value = mapToPropertyType(metadata.Text);
                setter(t, value);
            };
        }

        private static Action<ContentData, T> GetEnumerableMapping<T>(PropertyInfo propertyInfo, MetadataAttribute attribute) where T : new()
        {
            var mapToPropertyType = StringMapper.GetEnumerableMapping(propertyInfo.PropertyType);
            var setter = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (contentData, t) =>
            {
                var metadata = contentData.MetaData.SingleOrDefault(x => x.Name == attribute.FieldName);
                if (metadata == null) return;

                var splitString = metadata.Text.Split(metadata.Separator[0]);
                var value = mapToPropertyType(splitString);

                setter(t, value);
            };
        }
    }
}
