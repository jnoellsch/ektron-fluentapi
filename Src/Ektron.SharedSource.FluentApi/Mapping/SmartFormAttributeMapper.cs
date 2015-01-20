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
    public static class SmartFormAttributeMapper
    {
        public static Action<XNode, T> GetMapping<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var propertyMappings = new List<Action<XNode, T>>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<SmartFormAttributeAttribute>();
                if (attribute == null) continue;
                if (string.IsNullOrWhiteSpace(attribute.Xpath)) continue;

                var mapping = GetPropertyMapping<T>(propertyInfo, attribute.Xpath);
                propertyMappings.Add(mapping);
            }

            return (xml, t) => propertyMappings.ForEach(mapping => mapping(xml, t));
        }

        private static Action<XNode,T> GetPropertyMapping<T>(PropertyInfo propertyInfo, string xpath) where T : new()
        {
            var mapToPropertyType = StringMapper.GetMapping(propertyInfo.PropertyType);
            var setProperty = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

            return (xml, t) =>
            {
                var attrResult = (IEnumerable)xml.XPathEvaluate(xpath);
                var attr = attrResult.Cast<XAttribute>().FirstOrDefault();

                if (attr == null) return;

                var value = mapToPropertyType(attr.Value);

                setProperty(t, value);
            };
        }
    }
}
