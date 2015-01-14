using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    /// <summary>
    /// Provides a mapping from a string to a target type or enumerable of a type.
    /// </summary>
    internal static class StringMapper
    {
        /// <summary>
        /// Gets a mapping from a string to a primitive type.
        /// </summary>
        /// <param name="targetType">The type to map to.</param>
        /// <returns>An <see cref="Action"/> that maps the string to the <param name="targetType">target type</param>.</returns>
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

        /// <summary>
        /// Gets a mapping from an enumerable of strings to an enumerable instance of a primitive type.
        /// </summary>
        /// <param name="targetType">The type to map to.</param>
        /// <returns>An <see cref="Action"/> that maps the strings to the instances of the <param name="targetType">target type</param>.</returns>
        public static Func<IEnumerable<string>, object> GetEnumerableMapping(Type targetType)
        {
            var genericType = targetType.GetGenericArguments().Single();
            var genericMapping = GetMapping(genericType);

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(genericType);

            var addMethod = constructedListType.GetMethod("Add");

            return values =>
            {
                if (!values.Any()) return null;

                var instance = Activator.CreateInstance(constructedListType);

                foreach (var value in values)
                {
                    addMethod.Invoke(instance, new[] {genericMapping(value)});
                }

                return instance;
            };
        }

        /// <summary>
        /// Determines whether the target type is mappable by this class.
        /// </summary>
        /// <param name="targetType">The type to map.</param>
        /// <returns>A <see cref="Boolean"/> indicating whether this class can map that type.</returns>
        public static bool IsMappable(Type targetType)
        {
            return targetType.IsEnum || typeof(IConvertible).IsAssignableFrom(targetType);
        }

        /// <summary>
        /// Determines whether the target enumerable is mappable by this class
        /// </summary>
        /// <param name="targetType">The type to map.</param>
        /// <returns>A <see cref="Boolean"/> indicating whether this class can map that type.</returns>
        public static bool IsMappableEnumerable(Type targetType)
        {
            if (!typeof(IEnumerable).IsAssignableFrom(targetType)) return false;

            var genericType = targetType.GetGenericArguments().Single();

            return genericType.IsEnum || typeof(IConvertible).IsAssignableFrom(genericType);
        }
    }
}
