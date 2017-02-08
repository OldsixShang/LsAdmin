using System;

namespace Ls.Domain.UnitOfWork {
    /// <summary>
    /// 工作单元接口。
    /// </summary>
    public interface IUnitOfWork : IDisposable {
        /// <summary>
        /// 指示对象是否被释放。
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 使用默认选项开启工作单元。
        /// </summary>
        void Begin();

        /// <summary>
        /// 开启工作单元。
        /// </summary>
        /// <param name="options">工作单元选项</param>
        void Begin(UnitOfWorkOptions options);

        /// <summary>
        /// 提交工作单元。
        /// </summary>
        void Complete();
    }
}
