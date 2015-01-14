using System;
using System.Xml.Linq;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    /// <summary>
    /// Provides a mapping for Smart Form XML elements onto an object.
    /// </summary>
    internal static class SmartFormMapper
    {
        /// <summary>
        /// Gets a mapping from Smart Form XML elements to an instance of T.
        /// </summary>
        /// <typeparam name="T">The type being mapped to.</typeparam>
        /// <returns>An <see cref="Action"/> that maps Smart Form XML elements onto an instance of type T.</returns>
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
