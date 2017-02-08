namespace Ls.Components {
    /// <summary>
    /// 组件管理器接口。
    /// </summary>
    public interface IComponentManager {
        /// <summary>
        /// 初始化所有组件。
        /// </summary>
        void InitializeComponents();

        /// <summary>
        /// 关闭所有组件。
        /// </summary>
        void ShutdownComponents();
    }
}
