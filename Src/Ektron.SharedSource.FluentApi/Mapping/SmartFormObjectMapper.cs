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
            var subMapping = GetSubMapping(propertyType);
            var setter = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (xml, t) =>
            {
                var element = xml.XPathSelectElement(xpath);
                if (element == null) return;

                var complexType = Activator.CreateInstance(propertyType);
                subMapping.GetMethodInfo().Invoke(subMapping.Target, new[] { element, complexType });

                setter(t, complexType);
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
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(propertyType);
            var addMethod = constructedListType.GetMethod("Add");
            var subMapping = GetSubMapping(propertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (xml, t) =>
            {
                var elements = xml.XPathSelectElements(xpath).ToList();
                if (!elements.Any()) return;

                var listInstance = Activator.CreateInstance(constructedListType);

                foreach (var element in elements)
                {
                    var complexType = Activator.CreateInstance(propertyType);
                    subMapping.GetMethodInfo().Invoke(subMapping.Target, new[] { element, complexType });

                    addMethod.Invoke(listInstance, new[] { complexType });
                }

                setProperty(t, listInstance);
            };
        }

        /// <summary>
        /// Gets a mapping for mapping Smart Form data onto the sub-object.
        /// </summary>
        /// <param name="complexType">The <see cref="Type"/> of the sub-object.</param>
        /// <returns>An <see cref="Action"/> that maps the child Smart Form XML elements to the sub-object properties.</returns>
        public static Delegate GetSubMapping(Type complexType)
        {
            var subMappingMethod = typeof(SmartFormObjectMapper).GetMethod("GetSubMappingGeneric");
            var genericSubMappingMethod = subMappingMethod.MakeGenericMethod(complexType);
            
            return (Delegate)genericSubMappingMethod.Invoke(null, null);
        }

        /// <summary>
        /// A helper method that ensures field value and object Smart Form mappings are created.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <returns>An <see cref="Action"/> that maps the child Smart Form XML elements to the nested object properties.</returns>
        public static Action<XNode, T> GetSubMappingGeneric<T>() where T : new()
        {
            var complexType = typeof(T);

            var fieldValueGetMappingMethod = typeof(SmartFormFieldValueMapper).GetMethod("GetMapping");
            var genericFieldValueGetMappingMethod = fieldValueGetMappingMethod.MakeGenericMethod(complexType);

            var fieldValueMapping = (Action<XNode, T>)genericFieldValueGetMappingMethod.Invoke(null, null);

            var objectGetMappingMethod = typeof(SmartFormObjectMapper).GetMethod("GetMapping");
            var genericObjectGetMappingMethod = objectGetMappingMethod.MakeGenericMethod(complexType);

            var objectMapping = (Action<XNode, T>)genericObjectGetMappingMethod.Invoke(null, null);

            return (xml, t) =>
            {
                fieldValueMapping(xml, t);
                objectMapping(xml, t);
            };
        }
    }
}
