using System;
using System.Collections.Concurrent;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    public static class MappingRegistry
    {
        private static readonly ConcurrentDictionary<Type, object> _typeMappings = new ConcurrentDictionary<Type, object>();

        private static readonly object _lock = new object();

        public static Action<ContentData, T> GetMapping<T>() where T : new() 
        {
            var type = typeof(T);

            if (!_typeMappings.ContainsKey(type))
            {
                RegisterType<T>();
            }

            return (Action<ContentData, T>)_typeMappings[type];
        }

        public static void RegisterType<T>() where T : new()
        {
            var type = typeof(T);

            lock (_lock)
            {
                if (_typeMappings.ContainsKey(type)) return;

                var contentDataMapping = ContentDataMapper.GetMapping<T>();
                var metadataMapping = MetadataMapper.GetMapping<T>();
                var smartFormMapping = SmartFormMapper.GetMapping<T>();

                Action<ContentData, T> mapping = (contentData, t) =>
                {
                    contentDataMapping(contentData, t);
                    metadataMapping(contentData, t);
                    smartFormMapping(contentData, t);
                };

                _typeMappings[type] = mapping;
            }
        }
    }
}
