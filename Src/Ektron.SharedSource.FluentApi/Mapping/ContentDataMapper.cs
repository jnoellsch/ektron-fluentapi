using System;
using System.Collections.Generic;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    internal static class ContentDataMapper
    {
        public static Action<ContentData, T> GetMapping<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var propertyMappings = new List<Action<ContentData, T>>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<ContentDataAttribute>();
                if (attribute == null) continue;

                var sourcePropertyInfo = typeof(ContentData).GetProperty(attribute.PropertyName);
                if (sourcePropertyInfo == null) continue;

                if (sourcePropertyInfo.PropertyType != propertyInfo.PropertyType)
                    throw new Exception("To map ContentData properties, the source and destination types must match.");

                var propertyMapping = GetPropertyMapping<T>(sourcePropertyInfo, propertyInfo);

                propertyMappings.Add(propertyMapping);
            }

            return (contentData, t) => propertyMappings.ForEach(mapping => mapping(contentData, t));
        }

        private static Action<ContentData, T> GetPropertyMapping<T>(PropertyInfo sourcePropertyInfo, PropertyInfo propertyInfo) where T : new()
        {
            var getProperty = ExpressionUtil.GetPropertyGetter<ContentData>(sourcePropertyInfo);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (contentData, t) =>
            {
                var value = getProperty(contentData);
                setProperty(t, value);
            };
        }
    }
}
