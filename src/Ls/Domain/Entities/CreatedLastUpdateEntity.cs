using System.ComponentModel.DataAnnotations.Schema;
using Ls.Authorization;

namespace Ls.Domain.Entities
{
    /// <summary>
    /// 即需要创建者又需要 修改者 基类
    /// </summary>
    public class CreatedLastUpdateEntity : CreatedEntity,ILastUpdateBy
    {

        /// <summary>
        /// 最后一次修改者Id
        /// </summary>
        public long LastUpdaterId { get; set; }

        /// <summary>
        /// 最后一次修改者的 实体
        /// </summary>
        [ForeignKey("LastUpdaterId")]
        public virtual IUser LastUpdaterUser { get; set; }
    }
}