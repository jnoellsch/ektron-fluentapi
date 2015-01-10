using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    internal static class StringMapper
    {
        public static Func<string, object> GetMapping(Type targetType)
        {
            if (targetType.IsEnum)
            {
                return source => Enum.Parse(targetType, source);
            }
            else if (typeof(IConvertible).IsAssignableFrom(targetType))
            {
                return source => Convert.ChangeType(source, targetType);
            }
            else
            {
                throw new Exception("To convert from string, type must be IConvertible or Enum.");
            }
        }

        public static Func<IEnumerable<string>, object> GetEnumerableMapping(Type targetType)
        {
            var genericType = targetType.GetGenericArguments().Single();
            var genericMapping = GetMapping(genericType);

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(genericType);

            var add = constructedListType.GetMethod("Add");

            return values =>
            {
                if (!values.Any()) return null;

                var instance = Activator.CreateInstance(constructedListType);

                foreach (var value in values)
                {
                    add.Invoke(instance, new[] {genericMapping(value)});
                }

                return instance;
            };
        }

        public static bool IsMappable(Type targetType)
        {
            return targetType.IsEnum || typeof(IConvertible).IsAssignableFrom(targetType);
        }

        public static bool IsMappableEnumerable(Type targetType)
        {
            if (!typeof(IEnumerable).IsAssignableFrom(targetType)) return false;

            var genericType = targetType.GetGenericArguments().Single();

            return genericType.IsEnum || typeof(IConvertible).IsAssignableFrom(genericType);
        }
    }
}
