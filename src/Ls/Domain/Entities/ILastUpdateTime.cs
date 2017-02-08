using System;

namespace Ls.Domain.Entities
{
    /// <summary>
    /// 最后一次被修改时间
    /// </summary>
    public interface ILastUpdateTime
    {
        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        DateTime LastUpdateTime { get; set; }
    }
}