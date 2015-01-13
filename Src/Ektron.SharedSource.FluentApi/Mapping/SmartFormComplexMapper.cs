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
    internal static class SmartFormComplexMapper
    {
        public static Action<XNode, T> GetMapping<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var propertyMappings = new List<Action<XNode, T>>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<SmartFormComplexAttribute>();
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

        public static Delegate GetSubMapping(Type complexType)
        {
            var subMappingMethod = typeof(SmartFormComplexMapper).GetMethod("GetSubMappingGeneric");
            var genericSubMappingMethod = subMappingMethod.MakeGenericMethod(complexType);
            
            return (Delegate)genericSubMappingMethod.Invoke(null, null);
        }

        public static Action<XNode, T> GetSubMappingGeneric<T>() where T : new()
        {
            var complexType = typeof(T);

            var primitiveGetMappingMethod = typeof(SmartFormPrimitiveMapper).GetMethod("GetMapping");
            var genericPrimitiveGetMappingMethod = primitiveGetMappingMethod.MakeGenericMethod(complexType);

            var primitiveMapping = (Action<XNode, T>)genericPrimitiveGetMappingMethod.Invoke(null, null);

            var complexGetMappingMethod = typeof(SmartFormComplexMapper).GetMethod("GetMapping");
            var genericComplexGetMappingMethod = complexGetMappingMethod.MakeGenericMethod(complexType);

            var complexMapping = (Action<XNode, T>)genericComplexGetMappingMethod.Invoke(null, null);

            return (xml, t) =>
            {
                primitiveMapping(xml, t);
                complexMapping(xml, t);
            };
        }
    }
}
