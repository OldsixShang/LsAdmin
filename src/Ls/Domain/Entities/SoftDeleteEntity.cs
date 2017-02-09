namespace Ls.Domain.Entities {
    /// <summary>
    /// 软删除实体基类。
    /// </summary>
    public abstract class SoftDeleteEntity : Entity, ISoftDelete {
        /// <summary>
        /// 是否删除。
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
