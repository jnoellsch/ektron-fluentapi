﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using System.Xml.XPath;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mappers;
using Ektron.SharedSource.FluentApi.ModelAttributes;

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
        public static IEnumerable<T> AsContentType<T>(this IEnumerable<ContentData> source) where T : class
        {
            return source.Select(AsContentType<T>);
        }

        /// <summary>
        /// Converts a ContentData into some Content Type using Ektron deserialization.
        /// </summary>
        /// <typeparam name="T">Content Type to convert <see cref="ContentData"/> to.</typeparam>
        /// <param name="source">The <see cref="ContentData"/> to convert.</param>
        /// <returns>A content type.</returns>
        public static T AsContentType<T>(this ContentData source) where T : class
        {
            var result = Activator.CreateInstance<T>();

            var smartFormPrimitiveMapper = new SmartFormPrimitiveMapper();
            var smartFormComplexMapper = new SmartFormComplexMapper(smartFormPrimitiveMapper);
            var smartFormMapper = new SmartFormMapper(smartFormPrimitiveMapper, smartFormComplexMapper);
            var metadataMapper = new MetadataMapper();
            var contentDataMapper = new ContentDataMapper();

            smartFormMapper.Map(source, result);
            metadataMapper.Map(source, result);
            contentDataMapper.Map(source, result);

            return result;
        }
    }
}
