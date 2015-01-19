using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ektron.SharedSource.FluentApi.Mapping
{
    /// <summary>
    /// A helper class to help avoid using reflection during the actual mapping.
    /// </summary>
    internal static class ExpressionUtil
    {
        /// <summary>
        /// Gets a method that gets the value of the property from a specific instance of a class T.
        /// </summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <param name="propertyInfo">The property that holds the value to get.</param>
        /// <returns>A <see cref="Func"/> that can get the value.</returns>
        public static Func<T, object> GetPropertyGetter<T>(PropertyInfo propertyInfo) where T : new()
        {
            var tParam = Expression.Parameter(typeof(T), "t");
            var getBody = Expression.Convert(Expression.Property(tParam, propertyInfo), typeof(object));

            return Expression.Lambda<Func<T, object>>(getBody, tParam).Compile();
        }

        /// <summary>
        /// Gets a method that can set a property on an instance of a class T.
        /// </summary>
        /// <typeparam name="T">The type of the instance.</typeparam>
        /// <param name="propertyInfo">The property to set.</param>
        /// <returns>An <see cref="Action"/> that takes the value to set the property to.</returns>
        public static Action<T, object> GetPropertySetter<T>(PropertyInfo propertyInfo) where T : new()
        {
            var tParam = Expression.Parameter(typeof(T), "t");
            var valueParam = Expression.Parameter(typeof(object), "value");
            var setBody = Expression.Assign(Expression.Property(tParam, propertyInfo), Expression.Convert(valueParam, propertyInfo.PropertyType));

            return Expression.Lambda<Action<T, object>>(setBody, tParam, valueParam).Compile();
        }

        public static Func<T, object> GetMappingFromMethod<T>(MethodInfo methodInfo)
        {
            var methodCallExpression = Expression.Call(methodInfo);
            var getMapping = Expression.Lambda<Func<Func<T, object>>>(methodCallExpression).Compile();
            var mapping = getMapping();
            return mapping;
        }
    }
}
