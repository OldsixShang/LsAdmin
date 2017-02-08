using System;

namespace Ls.Domain.Entities
{
    /// <summary>
    /// 实体创建时间接口 
    /// </summary>
    public interface ICreatedTime
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreatedTime { get; set; }
    }
}