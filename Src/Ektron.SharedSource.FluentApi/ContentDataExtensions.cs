using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi
{
    /// <summary>
    /// Represents a set of extensions for <see cref="ContentData"/>.
    /// </summary>
    public static class ContentDataExtensions
    {
        /// <summary>
        /// Converts a set of ContentData into some content type using Ektron deserialization.
        /// </summary>
        /// <typeparam name="T">Content Type to convert <see cref="ContentData"/> to.</typeparam>
        /// <param name="source">A set of <see cref="ContentData"/> to convert.</param>
        /// <returns>A set of content types.</returns>
        public static IEnumerable<T> AsContentType<T>(this IEnumerable<ContentData> source)
        {
            return source.Select(c => c.AsContentType<T>());
        }

        /// <summary>
        /// Converts a ContentData into some Content Type using Ektron deserialization.
        /// </summary>
        /// <typeparam name="T">Content Type to convert <see cref="ContentData"/> to.</typeparam>
        /// <param name="source">The <see cref="ContentData"/> to convert.</param>
        /// <returns>A content type.</returns>
        public static T AsContentType<T>(this ContentData source)
        {
            return (T)EkXml.Deserialize(typeof(T), source.Html);
        }
    }
}
