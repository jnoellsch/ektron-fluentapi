using System.Collections;
using System.Linq;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    internal static class MetadataMapper
    {
        public static void Map<T>(ContentData source, T destination)
        {
            var properties = typeof(T).GetProperties();

            foreach (var propertyInfo in properties)
            {
                Map(source, propertyInfo, destination);
            }
        }

        private static void Map<T>(ContentData source, PropertyInfo property, T destination)
        {
            var metadataProperty = property.GetCustomAttributes<MetadataAttribute>().SingleOrDefault();

            if (metadataProperty == null) return;

            var metadata = source.MetaData.SingleOrDefault(x => x.Name == metadataProperty.FieldName);

            if (metadata == null) return;

            if (metadata.AllowMultiple && typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                var sources = metadata.Text.Split(metadata.Separator[0]);
                var values = StringMapper.Map(sources, property.PropertyType);
                property.SetValue(destination, values);
            }
            else
            {
                var value = StringMapper.Map(metadata.Text, property.PropertyType);
                property.SetValue(destination, value);
            }
        }
    }
}
