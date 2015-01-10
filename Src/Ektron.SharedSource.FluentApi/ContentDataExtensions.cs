using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mappers;

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
        public static IEnumerable<T> AsContentType<T>(this IEnumerable<ContentData> source) where T : new()
        {
            var mapper = ClassMappingRegistry.GetMapper<T>();

            return source.Select(x =>
            {
                var result = new T();
                mapper(x, result);

                return result;
            });
        }

        /// <summary>
        /// Converts a ContentData into some Content Type using Ektron deserialization.
        /// </summary>
        /// <typeparam name="T">Content Type to convert <see cref="ContentData"/> to.</typeparam>
        /// <param name="source">The <see cref="ContentData"/> to convert.</param>
        /// <returns>A content type.</returns>
        public static T AsContentType<T>(this ContentData source) where T : new()
        {
            var result = new T();
            var mapper = ClassMappingRegistry.GetMapper<T>();

            mapper(source, result);

            return result;
        }
    }
}
