using Ls.Domain.Entities;
using System;

namespace Ls.Domain.Repositories {
    /// <summary>
    /// 实体标识类型为 Int64 的仓储接口。
    /// </summary>
    /// <typeparam name="TEntity">实体类型参数</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, string>
        where TEntity : class, IEntity<string> {
    }
}
