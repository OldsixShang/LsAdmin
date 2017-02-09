using Ls.Domain.UnitOfWork;
using System;
using System.Data.Entity;

namespace Ls.EntityFramework.UnitOfWork {
    /// <summary>
    /// 工作单元扩展。
    /// </summary>
    public static class UnitOfWorkExtensions {
        /// <summary>
        /// 获取工作单元中的数据库上下文。
        /// </summary>
        /// <typeparam name="TDbContext"><see cref="DbContext"/>类型参数</typeparam>
        /// <param name="unitOfWork">工作单元</param>
        /// <returns>返回工作单元对应类型的<see cref="DbContext"/>对象。</returns>
        public static TDbContext GetDbContext<TDbContext>(this IUnitOfWork unitOfWork)
            where TDbContext : DbContext {
            if (unitOfWork == null) {
                throw new ArgumentException("unitOfWork");
            }
            if (!(unitOfWork is EfUnitOfWork)) {
                throw new LsException(string.Format("unitOfWork 不是 {0} 类型。", typeof(EfUnitOfWork).FullName));
            }
            return (unitOfWork as EfUnitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}
