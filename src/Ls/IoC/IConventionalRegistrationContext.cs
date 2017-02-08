using System.Reflection;

namespace Ls.IoC {
    /// <summary>
    /// 注册对象的上下文。
    /// </summary>
    public interface IConventionalRegistrationContext {
        /// <summary>
        /// 目标程序集。
        /// </summary>
        Assembly Assembly { get; }

        /// <summary>
        /// IoC 管理器。
        /// </summary>
        IIocManager IocManager { get; }
    }
}
