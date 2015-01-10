﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    internal static class SmartFormPrimitiveMapper
    {
        public static Action<XNode, T> GetMapping<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var propertyMappings = new List<Action<XNode, T>>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<SmartFormPrimitiveAttribute>();
                if (attribute == null) continue;
                if (string.IsNullOrWhiteSpace(attribute.Xpath)) continue;

                if (StringMapper.IsMappable(propertyInfo.PropertyType))
                {
                    propertyMappings.Add(GetPrimitiveMapping<T>(propertyInfo, attribute));
                }
                else if (StringMapper.IsMappableEnumerable(propertyInfo.PropertyType))
                {
                    propertyMappings.Add(GetEnumerableMapping<T>(propertyInfo, attribute));
                }
            }

            Action<XNode, T> combinedMapping =
                (xml, t) => propertyMappings.ForEach(mapping => mapping(xml, t));

            return combinedMapping;
        }

        private static Action<XNode, T> GetPrimitiveMapping<T>(PropertyInfo propertyInfo, SmartFormPrimitiveAttribute attribute) where T : new()
        {
            var mapToPropertyType = StringMapper.GetMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (xml, t) =>
            {
                var element = xml.XPathSelectElement(attribute.Xpath);
                if (element == null) return;

                var xmlText = element.Value;
                var value = mapToPropertyType(xmlText);

                setProperty(t, value);
            };
        }

        private static Action<XNode, T> GetEnumerableMapping<T>(PropertyInfo propertyInfo, SmartFormPrimitiveAttribute attribute) where T : new()
        {
            var mapToPropertyType = StringMapper.GetEnumerableMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (xml, t) =>
            {
                var elements = xml.XPathSelectElements(attribute.Xpath).ToList();
                if (!elements.Any()) return;

                var xmlTexts = elements.Select(x => x.Value);
                var value = mapToPropertyType(xmlTexts);

                setProperty(t, value);
            };
        }
    }
}
