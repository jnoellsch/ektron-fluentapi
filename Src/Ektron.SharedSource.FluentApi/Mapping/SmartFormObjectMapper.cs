using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    /// <summary>
    /// Provides a mapping Smart Form XML elements (i.e. has children) onto a nested custom class (i.e. has properties of it's own).
    /// </summary>
    internal static class SmartFormObjectMapper
    {
        /// <summary>
        /// Gets a mapping for properties from Smart Form elements to an instance of T.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <returns>An <see cref="Action"/> that maps Smart Form XML branches onto an instance of type T.</returns>
        public static Action<XNode, T> GetMapping<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var propertyMappings = new List<Action<XNode, T>>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<SmartFormObjectAttribute>();
                if (attribute == null) continue;
                if (string.IsNullOrWhiteSpace(attribute.Xpath)) continue;

                if (typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType))
                {
                    propertyMappings.Add(GetEnumerableMapping<T>(propertyInfo, attribute.Xpath));
                }
                else
                {
                    propertyMappings.Add(GetSingleMapping<T>(propertyInfo, attribute.Xpath));
                }
            }

            return (xml, t) => propertyMappings.ForEach(mapping => mapping(xml, t));
        }

        /// <summary>
        /// Gets a mapping for a single section (including properties on the object itself) of Smart Form XML onto a given property.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <param name="propertyInfo">The property on T.</param>
        /// <param name="xpath">The xpath representing the XML section to map.</param>
        /// <returns>An <see cref="Action"/> that maps a single Smart Form XML branch onto an instance of type T.</returns>
        public static Action<XNode, T> GetSingleMapping<T>(PropertyInfo propertyInfo, string xpath) where T : new()
        {
            var propertyType = propertyInfo.PropertyType;
            var setter = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            var getObjectCreatorMethod = typeof(SmartFormObjectMapper)
                .GetMethod("GetObjectCreator", BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(propertyType);

            var objectCreator = ExpressionUtil.GetMappingFromMethod<XNode>(getObjectCreatorMethod);

            return (xml, t) =>
            {
                var element = xml.XPathSelectElement(xpath);
                if (element == null) return;

                var obj = objectCreator(element);

                setter(t, obj);
            };
        }

        /// <summary>
        /// Gets a mapping for a multiple sections (including properties on the object itself) of Smart Form XML onto a given property.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <param name="propertyInfo">The property on T.</param>
        /// <param name="xpath">The xpath representing the XML section to map.</param>
        /// <returns>An <see cref="Action"/> that maps a single Smart Form XML branch onto an instance of type T.</returns>
        public static Action<XNode, T> GetEnumerableMapping<T>(PropertyInfo propertyInfo, string xpath) where T : new()
        {
            var propertyType = propertyInfo.PropertyType.GetGenericArguments().First();
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            var getObjectCreatorMethod = typeof(SmartFormObjectMapper)
                .GetMethod("GetEnumerableObjectCreator", BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(propertyType);

            var objectCreator = ExpressionUtil.GetMappingFromMethod<IEnumerable<XNode>>(getObjectCreatorMethod);

            return (xml, t) =>
            {
                var elements = xml.XPathSelectElements(xpath).ToList();
                if (!elements.Any()) return;

                var obj = objectCreator(elements);

                setProperty(t, obj);
            };
        }

        private static Func<XNode, T> GetObjectCreator<T>() where T : new()
        {
            var fieldValueMapping = SmartFormFieldValueMapper.GetMapping<T>();
            var objectMapping = GetMapping<T>();

            return xml =>
            {
                var obj = new T();

                fieldValueMapping(xml, obj);
                objectMapping(xml, obj);

                return obj;
            };
        }

        private static Func<IEnumerable<XNode>, IEnumerable<T>> GetEnumerableObjectCreator<T>() where T : new()
        {
            var creator = GetObjectCreator<T>();

            return xmls => xmls.Select(x => creator(x));
        }
    }
}
