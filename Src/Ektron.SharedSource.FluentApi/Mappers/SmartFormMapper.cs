using System;
using System.Xml.Linq;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    public class SmartFormMapper
    {
        private readonly SmartFormPrimitiveMapper _primitiveMapper;
        private readonly SmartFormComplexMapper _complexMapper;

        public SmartFormMapper(SmartFormPrimitiveMapper primitiveMapper, SmartFormComplexMapper complexMapper)
        {
            if (primitiveMapper == null) throw new ArgumentNullException("primitiveMapper");
            if (complexMapper == null) throw new ArgumentNullException("complexMapper");

            _primitiveMapper = primitiveMapper;
            _complexMapper = complexMapper;
        }

        public void Map<T>(ContentData source, T destination) where T : class
        {
            if (source == null) throw new ArgumentNullException("source");
            if (destination == null) throw new ArgumentNullException("xml");

            if (string.IsNullOrWhiteSpace(source.Html)) return;

            var xml = XDocument.Parse(source.Html);

            _complexMapper.Map(xml, destination);
            _primitiveMapper.Map(xml, destination);
        }
    }
}
