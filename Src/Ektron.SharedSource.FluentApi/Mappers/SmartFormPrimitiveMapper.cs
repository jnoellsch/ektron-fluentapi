using System;
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
            
            var node = xml.XPathSelectElement(attribute.Xpath);

            if (node == null) return;

            property.SetValue(destination, Convert.ChangeType(node.Value, property.PropertyType));
        }
    }
}
