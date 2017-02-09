namespace Ls.IoC {
    /// <summary>
    /// 依赖注册器接口。
    /// </summary>
    public interface IConventionalDependencyRegistrar {
        /// <summary>
        /// 根据上下文注册程序集。
        /// </summary>
        /// <param name="context">注册上下文</param>
        void RegisterAssembly(IConventionalRegistrationContext context);
    }
}
