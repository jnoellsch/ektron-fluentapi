using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    /// <summary>
    /// Represents the primary endpoint for mapping Ektron <see cref="ContentData"/>, Smart Form data, and metadata to custom types.
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// A collection to hold the registered mappings based on the type.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, object> _defaultTypeMappings = new ConcurrentDictionary<Type, object>();

        private static readonly ConcurrentDictionary<Type, List<object>> _additionalMappings = new ConcurrentDictionary<Type, List<object>>();

        /// <summary>
        /// A lock object to ensure multiple mappings for the same type aren't created.
        /// </summary>
        private static readonly object _defaultTypeMappingslock = new object();

        private static readonly object _additionalMappingslock = new object();

        /// <summary>
        /// Gets the registered mapping action for this <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="T">The class to which <see cref="ContentData"/> will be mapped.</typeparam>
        /// <returns>An <see cref="Action"/> that can map a <see cref="ContentData"/> onto an instance of the class.</returns>
        public static Action<ContentData, T> GetMapping<T>() where T : new()
        {
            var type = typeof(T);

            if (!_defaultTypeMappings.ContainsKey(type))
            {
                RegisterType<T>();
            }

            var defaultMapping = (Action<ContentData, T>) _defaultTypeMappings[type];

            if (!_additionalMappings.ContainsKey(type))
            {
                return defaultMapping;
            }
            else
            {
                return (contentData, t) =>
                {
                    defaultMapping(contentData, t);
                    _additionalMappings[type].ForEach(mapping => ((Action<ContentData, T>) mapping)(contentData, t));
                };
            }
        }

        /// <summary>
        /// Provides a way to manually create mappings ahead of getting them.
        /// </summary>
        /// <typeparam name="T">The class to which <see cref="ContentData"/> will be mapped.</typeparam>
        public static void RegisterType<T>() where T : new()
        {
            var type = typeof(T);

            lock (_defaultTypeMappingslock)
            {
                if (_defaultTypeMappings.ContainsKey(type)) return;

                var contentDataMapping = ContentDataMapper.GetMapping<T>();
                var metadataMapping = MetadataMapper.GetMapping<T>();
                var smartFormMapping = SmartFormMapper.GetMapping<T>();

                Action<ContentData, T> mapping = (contentData, t) =>
                {
                    if (contentData == null) throw new ArgumentNullException("contentData");
                    if (t == null) throw new ArgumentNullException("t");

                    contentDataMapping(contentData, t);
                    metadataMapping(contentData, t);
                    smartFormMapping(contentData, t);
                };

                _defaultTypeMappings[type] = mapping;
            }
        }

        public static void RegisterMapping<T>(Action<ContentData, T> mapping) where T : new()
        {
            var type = typeof(T);

            lock (_additionalMappingslock)
            {
                if (!_additionalMappings.ContainsKey(type))
                {
                    _additionalMappings[type] = new List<object>();
                }
            }

            _additionalMappings[type].Add(mapping);
        }
    }
}
