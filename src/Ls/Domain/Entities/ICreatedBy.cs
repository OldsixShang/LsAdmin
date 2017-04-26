namespace Ls.Domain.Entities
{
    /// <summary>
    /// 实体接口需要被创建者 接口
    /// </summary>
    public interface ICreatedBy
    {
        /// <summary>
        /// 创建者Id
        /// </summary>
        string CreaterId { get; set; }
    }
}