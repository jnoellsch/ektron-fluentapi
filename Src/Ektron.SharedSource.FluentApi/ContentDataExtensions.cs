using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi
{
    public static class ContentDataExtensions
    {
        public static IEnumerable<T> AsContentType<T>(this IEnumerable<ContentData> source)
        {
            return source.Select(c => c.AsContentType<T>());
        }

        public static T AsContentType<T>(this ContentData source)
        {
            return (T)EkXml.Deserialize(typeof(T), source.Html);
        }
    }
}
