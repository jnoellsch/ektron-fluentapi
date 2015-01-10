using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    internal static class ContentDataMapper
    {
        public static Action<ContentData, T> GetMapping<T>() where T : new()
        {
            var properties = typeof(T).GetProperties();
            var propertyMappings = new List<Action<ContentData, T>>();

            foreach (var propertyInfo in properties)
            {
                var attribute = propertyInfo.GetCustomAttribute<ContentDataAttribute>();
                if (attribute == null) continue;

                var sourceProperty = typeof(ContentData).GetProperty(attribute.PropertyName);
                if (sourceProperty == null) continue;

                if (sourceProperty.PropertyType != propertyInfo.PropertyType)
                    throw new Exception("To map ContentData properties, the source and destination types must match.");

                var getter = ExpressionUtil.GetPropertyGetter<ContentData>(sourceProperty);
                var setter = ExpressionUtil.GetPropertySetter<T>(propertyInfo);

                Action<ContentData, T> propertyMapping = (contentData, t) =>
                {
                    var value = getter(contentData);
                    setter(t, value);
                };

                //var tempPropertyInfo = propertyInfo;
                //Action<ContentData, T> propertyMapping = (contentData, t) =>
                //{
                //    var value = sourceProperty.GetValue(contentData);
                //    tempPropertyInfo.SetValue(t, value);
                //};
                
                propertyMappings.Add(propertyMapping);
            }

            return (contentData, t) => propertyMappings.ForEach(mapping => mapping(contentData, t));
        }
    }
}
