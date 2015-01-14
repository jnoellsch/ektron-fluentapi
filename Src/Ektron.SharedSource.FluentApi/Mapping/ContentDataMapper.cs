using System;
using System.Collections.Generic;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    /// <summary>
    /// Provides a mapping for <see cref="ContentData"/> onto an object.
    /// </summary>
    internal static class ContentDataMapper
    {
        /// <summary>
        /// Gets a mapping for properties from <see cref="ContentData"/> to an instance of T.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <returns>An <see cref="Action"/> that maps <see cref="ContentData"/> properties onto an instance of type T.</returns>
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

        /// <summary>
        /// Gets a mapping from a property on <see cref="ContentData"/> to a property on an instance of T.
        /// </summary>
        /// <typeparam name="T">The type that is being mapped to.</typeparam>
        /// <param name="sourcePropertyInfo">The property on the <see cref="ContentData"/>.</param>
        /// <param name="propertyInfo">The property on T.</param>
        /// <returns>An <see cref="Action"/> that maps a single property on <see cref="ContentData"/> to a single property on an instance of T.</returns>
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
