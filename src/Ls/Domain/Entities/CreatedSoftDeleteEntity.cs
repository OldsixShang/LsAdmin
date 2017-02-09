namespace Ls.Domain.Entities
{
    /// <summary>
    /// 需要创建者的 软删除实体 的基类
    /// </summary>
    public class CreatedSoftDeleteEntity:CreatedEntity,ISoftDelete
    {

        /// <summary>
        /// 软删除标志位
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}