using Ls.IoC;

namespace Ls.Domain.UnitOfWork {
    /// <summary>
    /// 工作单元提供者。
    /// </summary>
    public class UnitOfWorkProvider : IUnitOfWorkProvider {
        private IIocManager _iocManager;

        /// <summary>
        /// 创建<see cref="UnitOfWorkProvider"/>类型的对象。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        public UnitOfWorkProvider(IIocManager iocManager) {
            _iocManager = iocManager;
        }

        /// <summary>
        /// 获取当前上下文的工作单元。
        /// </summary>
        public IUnitOfWork Current { get { return UnitOfWorkCreator.UnitOfWork; } }

        /// <summary>
        /// 获取当前上下文的工作单元，若不存在，则创建新的。
        /// </summary>
        /// <returns>返回当前上下文的工作单元。</returns>
        public IUnitOfWork NewUnitOfWork() {
            if (UnitOfWorkCreator.UnitOfWork == null) {
                UnitOfWorkCreator.UnitOfWork = _iocManager.Resolve<IUnitOfWork>();
            }
            return UnitOfWorkCreator.UnitOfWork;
        }

    }
}