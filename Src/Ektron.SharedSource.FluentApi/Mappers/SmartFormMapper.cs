using System;
using System.Xml.Linq;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    internal static class SmartFormMapper
    {
        public static Action<ContentData, T> GetMapping<T>() where T : new()
        {
            var primitiveMapping = SmartFormPrimitiveMapper.GetMapping<T>();
            var complexMapping = SmartFormComplexMapper.GetMapping<T>();

            return (contentData, t) =>
            {
                if (string.IsNullOrWhiteSpace(contentData.Html)) return;
                var xml = XDocument.Parse(contentData.Html).Root;

                primitiveMapping(xml, t);
                complexMapping(xml, t);
            };
        }
    }
}
