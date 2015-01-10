using System;
using System.Collections.Concurrent;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    public static class MappingRegistry
    {
        private static readonly ConcurrentDictionary<Type, object> _typeMappers = new ConcurrentDictionary<Type, object>();

        private static readonly object _lock = new object();

        public static Action<ContentData, T> GetMapper<T>() where T : new() 
        {
            var type = typeof(T);

            if (!_typeMappers.ContainsKey(type))
            {
                RegisterType<T>();
            }

            return (Action<ContentData, T>)_typeMappers[type];
        }

        public static void RegisterType<T>() where T : new()
        {
            var type = typeof(T);

            lock (_lock)
            {
                if (_typeMappers.ContainsKey(type)) return;

                var contentDataMapping = ContentDataMapper.GetMapping<T>();
                var metadataMapping = MetadataMapper.GetMapping<T>();
                var smartFormMapping = SmartFormMapper.GetMapping<T>();

                Action<ContentData, T> mapper = (contentData, t) =>
                {
                    contentDataMapping(contentData, t);
                    metadataMapping(contentData, t);
                    smartFormMapping(contentData, t);
                };

                _typeMappers[type] = mapper;
            }
        }
    }
}
