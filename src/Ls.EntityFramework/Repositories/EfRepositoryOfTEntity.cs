using Ls.Domain.Entities;
using Ls.Domain.Repositories;
using System;
using System.Data.Entity;

namespace Ls.EntityFramework.Repositories {
    /// <summary>
    /// EF 仓储基类。
    /// </summary>
    /// <typeparam name="TDbContext"><see cref="DbContext"/>类型参数</typeparam>
    /// <typeparam name="TEntity"><see cref="IEntity{TId}"/>类型参数</typeparam>
    public class EfRepository<TDbContext, TEntity> : EfRepository<TDbContext, TEntity, string>, IRepository<TEntity>
        where TEntity : class, IEntity<string>
        where TDbContext : DbContext {
        
   }
}
