using Ls.Reflection;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace Ls.EntityFramework.UnitOfWork {
    /// <summary>
    /// <see cref="DbContext"/>扩展方法。
    /// </summary>
    public static class DbContextExtensions {
        /// <summary>
        /// 获取<see cref="DbContext"/>中的实体类型列表。
        /// </summary>
        /// <param name="dbContextType"><see cref="DbContext"/>类型</param>
        /// <returns>返回<see cref="DbContext"/>中的实体列表。</returns>
        public static IEnumerable<Type> GetEntityTypes(this Type dbContextType) {
            return
                from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                where
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(IDbSet<>)) ||
                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>))
                select property.PropertyType.GenericTypeArguments[0];
        }
    }
}
