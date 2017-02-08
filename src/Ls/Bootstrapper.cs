using Ls.Components;
using Ls.IoC;
using System;

namespace Ls {
    /// <summary>
    /// Ls 启动器。
    /// </summary>
    public class Bootstrapper : IDisposable {
        private IComponentManager _componentManager;

        /// <summary>
        /// 指示对象是否被释放。
        /// </summary>
        protected bool IsDisposed;

        /// <summary>
        /// IoC 管理器。
        /// </summary>
        public IIocManager IocManager { get; private set; }

        /// <summary>
        /// Ls 构造函数。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        public Bootstrapper(IIocManager iocManager) {
            IocManager = iocManager;
        }

        /// <summary>
        /// Ls 构造函数。
        /// </summary>
        public Bootstrapper()
            : this(IoC.IocManager.Instance) {

        }

        /// <summary>
        /// 初始化 Ls 框架。
        /// </summary>
        public virtual void Initialize() {
            IocManager.IocContainer.Install(new LsInstaller());

            _componentManager = IocManager.Resolve<IComponentManager>();
            _componentManager.InitializeComponents();
        }

        /// <summary>
        /// 框架释放。
        /// </summary>
        public void Dispose() {
            if (!IsDisposed) {
                IsDisposed = true;
                if (_componentManager != null) {
                    _componentManager.ShutdownComponents();
                }
            }
        }

    }
}
