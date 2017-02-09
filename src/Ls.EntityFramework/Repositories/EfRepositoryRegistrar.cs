using Ls.Domain.Entities;
using Ls.Domain.Repositories;
using Ls.EntityFramework.UnitOfWork;
using Ls.IoC;
using System;

namespace Ls.EntityFramework.Repositories {
    /// <summary>
    /// EF 仓储注册器。
    /// </summary>
    internal static class EfRepositoryRegistrar {
        /// <summary>
        /// 将<paramref name="dbContextType"/>类型对应的实体仓储注册到<paramref name="iocManager"/>对象中。
        /// </summary>
        /// <param name="dbContextType">数据库上下文类型</param>
        /// <param name="iocManager">IoC 管理器</param>
        public static void RegisterForDbContext(Type dbContextType, IIocManager iocManager) {
            // 遍历 DbContext 的所有实体
            foreach (var entityType in dbContextType.GetEntityTypes()) {
                // 遍历实体继承的所有接口
                foreach (var interfaceType in entityType.GetInterfaces()) {
                    // 判断泛型接口且接口类型为IEntity<>
                    if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>)) {
                        // 获取实体主键类型
                        var primaryKeyType = interfaceType.GenericTypeArguments[0];

                        if (primaryKeyType == typeof(Int64)) {
                            var genericRepositoryType = typeof(IRepository<>).MakeGenericType(entityType);
                            if (!iocManager.IsRegistered(genericRepositoryType)) {
                                iocManager.Register(
                                    genericRepositoryType,
                                    typeof(EfRepository<,>).MakeGenericType(dbContextType, entityType),
                                    DependencyLifeCycle.Transient
                                    );
                               
                            }
                        }

                        var genericRepositoryTypeWithPrimaryKey = typeof(IRepository<,>).MakeGenericType(entityType, primaryKeyType);
                        if (!iocManager.IsRegistered(genericRepositoryTypeWithPrimaryKey)) {
                            iocManager.Register(
                                genericRepositoryTypeWithPrimaryKey,
                                typeof(EfRepository<,,>).MakeGenericType(dbContextType, entityType, primaryKeyType),
                                DependencyLifeCycle.Transient
                                );
                            
                        } else {
                            var obj = iocManager.Resolve(genericRepositoryTypeWithPrimaryKey);
                        }

                    }
                }
            }
        }
    }
}

