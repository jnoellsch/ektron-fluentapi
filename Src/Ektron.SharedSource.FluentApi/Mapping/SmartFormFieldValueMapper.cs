using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    /// <summary>
    /// Provides a mapping for Smart Form XML field values onto a property
    /// </summary>
    internal static class SmartFormFieldValueMapper
    {
        /// <summary>
        /// Gets a mapping from all mapped Smart Form XML field values to the properties on an instance of T.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <returns>An <see cref="Action"/> that maps the field values from the Smart Form XML to the instance of T.</returns>
        public static Action<XNode, T> GetMapping<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var propertyMappings = new List<Action<XNode, T>>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<SmartFormFieldValueAttribute>();
                if (attribute == null) continue;
                if (string.IsNullOrWhiteSpace(attribute.Xpath)) continue;

                if (StringMapper.IsMappable(propertyInfo.PropertyType))
                {
                    propertyMappings.Add(GetSingleMapping<T>(propertyInfo, attribute.Xpath));
                }
                else if (StringMapper.IsMappableEnumerable(propertyInfo.PropertyType))
                {
                    propertyMappings.Add(GetEnumerableMapping<T>(propertyInfo, attribute.Xpath));
                }
            }

            return (xml, t) => propertyMappings.ForEach(mapping => mapping(xml, t));
        }

        /// <summary>
        /// Gets a mapping from a single Smart Form XML element to a property on an instance of T.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <param name="propertyInfo">The property on T being mapped to.</param>
        /// <param name="xpath">The xpath for the field values being mapped.</param>
        /// <returns>An <see cref="Action"/> that maps a field value from the Smart Form XML to a property on an instance of T.</returns>
        private static Action<XNode, T> GetSingleMapping<T>(PropertyInfo propertyInfo, string xpath) where T : new()
        {
            var mapToPropertyType = StringMapper.GetMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (xml, t) =>
            {
                var element = xml.XPathSelectElement(xpath);
                if (element == null) return;

                var text = GetXmlInnerText(element);
                var value = mapToPropertyType(text);

                setProperty(t, value);
            };
        }

        /// <summary>
        /// Gets a mapping from multiple matching elements Smart Form XML elements to a property on an instance of T.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <param name="propertyInfo">The property on T being mapped to.</param>
        /// <param name="xpath">The xpath for the field values being mapped.</param>
        /// <returns>An <see cref="Action"/> that maps an multiple matching fields from the Smart Form XML to a property on an instance of T.</returns>
        private static Action<XNode, T> GetEnumerableMapping<T>(PropertyInfo propertyInfo, string xpath) where T : new()
        {
            var mapToPropertyType = StringMapper.GetEnumerableMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (xml, t) =>
            {
                var elements = xml.XPathSelectElements(xpath).ToList();
                if (!elements.Any()) return;

                var rawValues = elements.Select(GetXmlInnerText);
                var values = mapToPropertyType(rawValues);

                setProperty(t, values);
            };
        }

        private static string GetXmlInnerText(XElement element)
        {
            return element.HasElements ? String.Concat(element.Nodes()) : element.Value;
        }
    }
}
