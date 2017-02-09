namespace Ls.Domain.Entities
{
    /// <summary>
    /// 最后一次修改者接口
    /// </summary>
    public interface ILastUpdateBy
    {
        /// <summary>
        /// 最后一次修改者Id
        /// </summary>
        long LastUpdaterId { get; set; }
    }
}