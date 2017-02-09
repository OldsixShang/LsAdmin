namespace Ls.Domain.Repositories {
    /// <summary>
    /// 排序顺序。
    /// </summary>
    public enum SortOrder {
        /// <summary>
        /// 默认排序，不对数据应用排序规则。
        /// </summary>
        Default = -1,

        /// <summary>
        /// 顺序排序。
        /// </summary>
        Asc,

        /// <summary>
        /// 逆序排序。
        /// </summary>
        Desc
    }
}
