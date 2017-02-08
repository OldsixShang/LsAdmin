namespace Ls.Domain.Entities {
    /// <summary>
    /// 软删除实体基类。
    /// </summary>
    /// <typeparam name="TId">实体主键类型参数</typeparam>
    public abstract class SoftDeleteEntity<TId> : Entity<TId>, ISoftDelete {
        /// <summary>
        /// 是否删除。
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
