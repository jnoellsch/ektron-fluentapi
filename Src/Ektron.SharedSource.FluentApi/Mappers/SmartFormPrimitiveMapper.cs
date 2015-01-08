using System;
using System.Collections;
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
            if (destination == null) throw new ArgumentNullException("destination");

            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties)
            {
                MapProperty(xml, propertyInfo, destination);
            }
        }

        private void MapProperty<T>(XNode xml, PropertyInfo property, T destination)
        {
            var attribute = property.GetCustomAttributes<SmartFormPrimitiveAttribute>().SingleOrDefault();

            if (attribute == null) return;

            var xpath = attribute.Xpath;

            if (typeof (IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                MapEnumerable(xml, property, destination, xpath);
            }
            else
            {
                MapBasic(xml, property, destination, xpath);
            }
        }

        private static void MapBasic<T>(XNode xml, PropertyInfo property, T destination, string xpath)
        {
            var node = xml.XPathSelectElement(xpath);

            if (node == null) return;

            var value = StringMapper.Map(node.Value, property.PropertyType);

            property.SetValue(destination, value);
        }

        private static void MapEnumerable<T>(XNode xml, PropertyInfo property, T destination, string xpath)
        {
            var nodes = xml.XPathSelectElements(xpath).Select(x => x.Value).ToList();

            var value = StringMapper.Map(nodes, property.PropertyType);

            property.SetValue(destination, value);
        }
    }
}
