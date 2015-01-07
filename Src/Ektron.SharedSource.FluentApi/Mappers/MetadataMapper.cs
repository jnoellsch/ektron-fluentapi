using System;
using System.Linq;
using System.Reflection;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    public class MetadataMapper
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
            var metadataProperty = property.GetCustomAttributes<MetadataAttribute>().SingleOrDefault();

            if (metadataProperty == null) return;

            var metadata = source.MetaData.SingleOrDefault(x => x.Name == metadataProperty.FieldName);

            if (metadata == null) return;

            property.SetValue(destination, Convert.ChangeType(metadata.Text, property.PropertyType));
        }
    }
}
