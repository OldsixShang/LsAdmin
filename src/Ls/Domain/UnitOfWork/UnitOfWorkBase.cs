using Ls.IoC;

namespace Ls.Domain.UnitOfWork {
    /// <summary>
    /// 工作单元基类。
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork, ITransientDependency {
        /// <summary>
        /// 指示对象是否被释放。
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 当前工作单元选项。
        /// </summary>
        public UnitOfWorkOptions Options { get; set; }

        /// <summary>
        /// 使用默认选项开启工作单元。
        /// </summary>
        public void Begin() {
            Begin(new UnitOfWorkOptions());
        }

        /// <summary>
        /// 开启工作单元。
        /// </summary>
        /// <param name="options">工作单元选项</param>
        public abstract void Begin(UnitOfWorkOptions options);

        /// <summary>
        /// 提交工作单元。
        /// </summary>
        public abstract void Complete();

        /// <summary>
        /// 释放工作单元中的对象。
        /// </summary>
        protected abstract void DisposeUnitOfWork();

        /// <summary>
        /// 释放工作单元。
        /// </summary>
        public void Dispose() {
            if (!IsDisposed) {
                IsDisposed = true;
                DisposeUnitOfWork();
            }
        }

    }
}
