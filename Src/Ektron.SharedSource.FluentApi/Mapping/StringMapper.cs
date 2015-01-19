using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
            var mapping = GetMapping(genericType);

            var getEnumerableCreator = typeof(StringMapper)
                .GetMethod("GetEnumerableCreator", BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(genericType);

            var mappingParam = Expression.Constant(mapping);
            var stringsParam = Expression.Parameter(typeof(IEnumerable<string>), "strings");
            var methodExpression = Expression.Call(getEnumerableCreator, mappingParam, stringsParam);

            var lambda = Expression.Lambda<Func<IEnumerable<String>, object>>(methodExpression, stringsParam).Compile();

            return values =>
            {
                if (!values.Any()) return null;

                var obj = lambda(values);

                return obj;
            };
        }

        private static object GetEnumerableCreator<T>(Func<string, object> mapping, IEnumerable<string> strings)
        {
            return strings.Select(x => (T)mapping(x)).ToList();
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
