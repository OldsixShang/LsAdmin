namespace Ls.Domain.Entities
{
    /// <summary>
    /// 包含 创建者 、修改者、软删除 的实体的基类
    /// </summary>
    public class CreatedLastUpdateSoftDeleteEntity : CreatedLastUpdateEntity,ISoftDelete
    {

        /// <summary>
        /// 软删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}