using Ls.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Extensions
{
    public static class IQueryableExtension
    {
        private static IQueryable<T> InternalOrder<T>(this IQueryable<T> source, String propertyName, String methodName)
        {
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "p");
            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null) throw new ArgumentNullException("此列不存在");
            Expression expr = Expression.Property(arg, property);
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), property.PropertyType);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            return ((IQueryable<T>)(typeof(Queryable).GetMethods().Single(
                p => String.Equals(p.Name, methodName, StringComparison.Ordinal)
                    && p.IsGenericMethodDefinition
                    && p.GetGenericArguments().Length == 2
                    && p.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.PropertyType)
                .Invoke(null, new Object[] { source, lambda })));
        }

        public static IQueryable<T> Page<T>(this IQueryable<T> source, Pager pager)
        {
            pager.totalCount = source.Count<T>();
            if (pager.order == "asc")
                source = source.InternalOrder<T>(pager.sort, "OrderBy");
            else
                source = source.InternalOrder<T>(pager.sort, "OrderByDescending");
            return source.Take(pager.page * pager.rows)
                .Skip((pager.page - 1) * pager.rows);
        }
    }
}
