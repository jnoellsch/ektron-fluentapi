﻿using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ektron.SharedSource.FluentApi.Mappers
{
    internal static class ExpressionUtil
    {
        public static Func<T, object> GetPropertyGetter<T>(PropertyInfo propertyInfo) where T : new()
        {
            var tParam = Expression.Parameter(typeof(T), "t");
            var getBody = Expression.Property(tParam, propertyInfo);

            return Expression.Lambda<Func<T, object>>(getBody, tParam).Compile();
        }

        public static Action<T, object> GetPropertySetter<T>(PropertyInfo propertyInfo) where T : new()
        {
            var tParam = Expression.Parameter(typeof(T), "t");
            var valueParam = Expression.Parameter(typeof(object), "value");
            var setBody = Expression.Assign(Expression.Property(tParam, propertyInfo), valueParam);

            return Expression.Lambda<Action<T, object>>(setBody, tParam, valueParam).Compile();
        }
    }
}