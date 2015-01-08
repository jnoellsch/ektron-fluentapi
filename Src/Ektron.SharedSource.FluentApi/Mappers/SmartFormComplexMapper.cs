using System;
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
        public static void Map<T>(XNode xml, T destination) where T : class
        {
            if (xml == null) throw new ArgumentNullException("xml");
            if (destination == null) throw new ArgumentNullException("xml");

            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties)
            {
                MapProperty(xml, propertyInfo, destination);
            }
        }

        private static void MapProperty<T>(XNode xml, PropertyInfo property, T destination)
        {
            var attribute = property.GetCustomAttributes<SmartFormComplexAttribute>().SingleOrDefault();

            if (attribute == null) return;

            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                MapFromArray(xml, property, destination, attribute);
            }
            else
            {
                MapFromValue(xml, property, destination, attribute);
            }
        }

        private static void MapFromValue(XNode xml, PropertyInfo property, object destination, SmartFormComplexAttribute attribute)
        {
            var node = xml.XPathSelectElement(attribute.Xpath);

            if (node == null) return;

            var complex = Activator.CreateInstance(property.PropertyType);

            property.SetValue(destination, complex);

            var map = typeof(SmartFormPrimitiveMapper).GetMethod("Map").MakeGenericMethod(property.PropertyType);
            map.Invoke(null, new[] { node, complex });

            Map(node, complex);
        }

        private static void MapFromArray(XNode xml, PropertyInfo property, object destination, SmartFormComplexAttribute attribute)
        {
            var nodes = xml.XPathSelectElements(attribute.Xpath).ToList();

            if (!nodes.Any()) return;

            var propertyType = property.PropertyType.GetGenericArguments().First();

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(propertyType);

            var instance = Activator.CreateInstance(constructedListType);
            var add = constructedListType.GetMethod("Add");
            var map = typeof(SmartFormPrimitiveMapper).GetMethod("Map").MakeGenericMethod(propertyType);

            foreach (var node in nodes)
            {
                var complex = Activator.CreateInstance(propertyType);
                map.Invoke(null, new[] { node, complex });

                Map(node, complex);

                add.Invoke(instance, new[] { complex });
            }

            property.SetValue(destination, instance);
        }
    }
}
