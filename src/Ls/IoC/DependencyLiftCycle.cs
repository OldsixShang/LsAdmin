namespace Ls.IoC {
    /// <summary>
    /// 依赖注入生命周期枚举。
    /// </summary>
    public enum DependencyLifeCycle {
        /// <summary>
        /// 单例模式。
        /// </summary>
        Singleton,
        /// <summary>
        /// 实例模式。
        /// </summary>
        Transient
    }
}
