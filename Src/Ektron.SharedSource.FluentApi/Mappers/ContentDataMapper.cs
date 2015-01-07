using System;
using System.Linq;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    public class ContentDataMapper
    {
        public void Map<T>(ContentData source, T destination)
        {
            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties)
            {
                Map(source, propertyInfo, destination);
            }
        }

        private void Map<T>(ContentData source, PropertyInfo property, T destination)
        {
            var contentDataProperty = property.GetCustomAttributes<ContentDataAttribute>().SingleOrDefault();

            if (contentDataProperty == null) return;

            var sourceProperty = typeof(ContentData).GetProperty(contentDataProperty.PropertyName);

            if (sourceProperty == null) return;

            var value = sourceProperty.GetValue(source);
            property.SetValue(destination, Convert.ChangeType(value, property.PropertyType));
        }
    }
}
