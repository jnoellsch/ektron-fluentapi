using System;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms.Common;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    public class SmartFormComplexMapper
    {
        private readonly SmartFormPrimitiveMapper _primitiveMapper;

        public SmartFormComplexMapper(SmartFormPrimitiveMapper primitiveMapper)
        {
            if (primitiveMapper == null) throw new ArgumentNullException("primitiveMapper");

            _primitiveMapper = primitiveMapper;
        }

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
            var attribute = property.GetCustomAttributes<SmartFormComplexAttribute>().SingleOrDefault();

            if (attribute == null) return;
            
            var node = xml.XPathSelectElement(attribute.Xpath);

            if (node == null) return;

            var complex = Activator.CreateInstance(property.PropertyType);

            property.SetValue(destination, complex);

            var map = _primitiveMapper.GetType().GetMethod("Map").MakeGenericMethod(property.PropertyType);
            map.Invoke(_primitiveMapper, new[] { node, complex });

            this.Map(node, complex);
        }
    }
}
