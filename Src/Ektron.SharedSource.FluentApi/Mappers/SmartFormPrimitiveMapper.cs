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
    public class SmartFormPrimitiveMapper
    {
        public void Map<T>(XNode xml, T destination) where T : class
        {
            if (xml == null) throw new ArgumentNullException("xml");
            if (destination == null) throw new ArgumentNullException("xml");

            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties)
            {
                Map(xml, propertyInfo, destination);
            }
        }

        private void Map<T>(XNode xml, PropertyInfo property, T destination)
        {
            var attribute = property.GetCustomAttributes<SmartFormPrimitiveAttribute>().SingleOrDefault();

            if (attribute == null) return;

            if (typeof (IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                MapFromArray(xml, property, destination, attribute);
            }
            else
            {
                MapFromValue(xml, property, destination, attribute);
            }
        }

        private static void MapFromValue<T>(XNode xml, PropertyInfo property, T destination, SmartFormPrimitiveAttribute attribute)
        {
            var node = xml.XPathSelectElement(attribute.Xpath);

            if (node == null) return;

            property.SetValue(destination, Convert.ChangeType(node.Value, property.PropertyType));
        }

        private static void MapFromArray<T>(XNode xml, PropertyInfo property, T destination, SmartFormPrimitiveAttribute attribute)
        {
            var nodes = xml.XPathSelectElements(attribute.Xpath).ToList();

            if (!nodes.Any()) return;
            
            var propertyType = property.PropertyType.GetGenericArguments().First();

            var listType = typeof (List<>);
            var constructedListType = listType.MakeGenericType(propertyType);

            var instance = Activator.CreateInstance(constructedListType);
            var add = constructedListType.GetMethod("Add");

            foreach (var node in nodes)
            {
                var value = Convert.ChangeType(node.Value, propertyType);
                add.Invoke(instance, new[] {value});
            }

            property.SetValue(destination, instance);
        }
    }
}
