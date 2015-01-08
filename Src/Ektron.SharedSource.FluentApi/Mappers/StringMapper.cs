using System;
using System.Collections.Generic;
using System.Linq;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    public static class StringMapper
    {
        public static Object Map(string value, Type targetType)
        {
            if (targetType.IsEnum)
            {
                return Enum.Parse(targetType, value);
            }
            else if (typeof(IConvertible).IsAssignableFrom(targetType))
            {
                return Convert.ChangeType(value, targetType);
            }
            else
            {
                throw new Exception("To convert from string, type must be IConvertible (string, int, DateTime), IEnumerable<IConvertible>, or an enum.");
            }
        }

        public static Object Map(IEnumerable<string> values, Type targetType)
        {
            if (!values.Any()) return null;

            var genericType = targetType.GetGenericArguments().First();

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(genericType);

            var instance = Activator.CreateInstance(constructedListType);
            var add = constructedListType.GetMethod("Add");

            foreach (var value in values)
            {
                add.Invoke(instance, new[] { Map(value, genericType) });
            }

            return instance;
        }
    }
}
