using Ls.IoC;

namespace Ls.Domain.UnitOfWork {
    /// <summary>
    /// 工作单元提供者接口。
    /// </summary>
    public interface IUnitOfWorkProvider : ITransientDependency {
        /// <summary>
        /// 获取当前上下文的工作单元。
        /// </summary>
        IUnitOfWork Current { get; }

        /// <summary>
        /// 获取当前上下文的工作单元，若不存在，则创建新的。
        /// </summary>
        /// <returns>返回当前上下文的工作单元。</returns>
        IUnitOfWork NewUnitOfWork();
    }
}
