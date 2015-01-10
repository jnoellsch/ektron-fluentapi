﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi.Mappers
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
                    propertyMappings.Add(GetBasicMapping<T>(propertyInfo, attribute.Xpath));
                }
            }

            Action<XNode, T> combinedMapping =
                (xml, t) => propertyMappings.ForEach(mapping => mapping(xml, t));

            return combinedMapping;
        }

        public static Action<XNode, T> GetBasicMapping<T>(PropertyInfo propertyInfo, string xpath) where T : new()
        {
            var propertyType = propertyInfo.PropertyType;
            var subMapping = GetSubMapping(propertyType);

            return (xml, t) =>
            {
                var node = xml.XPathSelectElement(xpath);
                if (node == null) return;

                var complexType = Activator.CreateInstance(propertyType);
                subMapping.GetMethodInfo().Invoke(subMapping.Target, new[] { node, complexType });

                propertyInfo.SetValue(t, complexType);
            };
        }

        public static Action<XNode, T> GetEnumerableMapping<T>(PropertyInfo propertyInfo, string xpath) where T : new()
        {
            var propertyType = propertyInfo.PropertyType.GetGenericArguments().First();
            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(propertyType);
            var addMethod = constructedListType.GetMethod("Add");
            var subMapping = GetSubMapping(propertyType);

            return (xml, t) =>
            {
                var nodes = xml.XPathSelectElements(xpath).ToList();
                if (!nodes.Any()) return;

                var listInstance = Activator.CreateInstance(constructedListType);

                foreach (var node in nodes)
                {
                    var complexType = Activator.CreateInstance(propertyType);
                    subMapping.GetMethodInfo().Invoke(subMapping.Target, new[] { node, complexType });

                    addMethod.Invoke(listInstance, new[] { complexType });
                }

                propertyInfo.SetValue(t, listInstance);
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
