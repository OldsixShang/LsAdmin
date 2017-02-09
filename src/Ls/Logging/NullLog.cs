

namespace Ls.Logging {
    /// <summary>
    /// 空日志实现。
    /// </summary>
    public static class NullLog
    {
        /// <summary>
        /// 空日志实例。
        /// </summary>
        public static ILog Instance = null; // LogManager.GetLogger("");
    }
}
