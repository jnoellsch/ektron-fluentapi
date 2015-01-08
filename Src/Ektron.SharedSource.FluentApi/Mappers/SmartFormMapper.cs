using System;
using System.Xml.Linq;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    internal static class SmartFormMapper
    {
        public static void Map<T>(ContentData source, T destination) where T : class
        {
            if (source == null) throw new ArgumentNullException("source");
            if (destination == null) throw new ArgumentNullException("xml");

            if (string.IsNullOrWhiteSpace(source.Html)) return;

            var xml = XDocument.Parse(source.Html).Root;

            SmartFormComplexMapper.Map(xml, destination);
            SmartFormPrimitiveMapper.Map(xml, destination);
        }
    }
}
