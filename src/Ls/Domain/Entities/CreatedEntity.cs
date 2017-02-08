using System.ComponentModel.DataAnnotations.Schema;
using Ls.Authorization;

namespace Ls.Domain.Entities
{
    /// <summary>
    /// 需要创建者的实体 的基类
    /// </summary>
    public class CreatedEntity:Entity,ICreatedBy
    {

        /// <summary>
        /// 创建者Id
        /// </summary>
        public long CreaterId { get; set; }

        /// <summary>
        /// 创建者 实体
        /// </summary>
        [ForeignKey("CreaterId")]
        public virtual IUser  CreaterUser { get; set; }
    }
}