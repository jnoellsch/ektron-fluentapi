using System;
using System.Collections.Generic;
using Ektron.Cms;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    public static class ClassMappingRegistry
    {
        private static readonly Dictionary<Type, object> _typeMappers = new Dictionary<Type, object>();

        public static Action<ContentData, T> GetMapper<T>() where T : new() 
        {
            var type = typeof(T);

            if (_typeMappers.ContainsKey(type))
            {
                return (Action<ContentData, T>) _typeMappers[type];
            }

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

            return mapper;
        }
    }
}
