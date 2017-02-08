using System.Reflection;

namespace Ls.IoC {
    /// <summary>
    /// 注册对象的上下文。
    /// </summary>
    internal class ConventionalRegistrationContext : IConventionalRegistrationContext {
        /// <summary>
        /// 目标程序集。
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// IoC 管理器。
        /// </summary>
        public IIocManager IocManager { get; set; }

        /// <summary>
        /// 上下文构造器。
        /// </summary>
        /// <param name="assembly">目标程序集</param>
        /// <param name="iocManager">IoC 管理器</param>
        internal ConventionalRegistrationContext(Assembly assembly, IIocManager iocManager) {
            Assembly = assembly;
            IocManager = iocManager;
        }
    }
}
