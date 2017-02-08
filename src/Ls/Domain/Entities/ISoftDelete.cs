namespace Ls.Domain.Entities {
    /// <summary>
    /// 软删除接口。
    /// </summary>
    public interface ISoftDelete {
        /// <summary>
        /// 标记对象是否已被删除。
        /// </summary>
        bool IsDeleted { get; set; }
    }
}
