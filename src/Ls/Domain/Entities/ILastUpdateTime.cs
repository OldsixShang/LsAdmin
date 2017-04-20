using System;

namespace Ls.Domain.Entities
{
    /// <summary>
    /// 最后一次被修改时间
    /// </summary>
    public interface ILastUpdatedTime
    {
        /// <summary>
        /// 最后一次更新时间
        /// </summary>
        DateTime LastUpdatedTime { get; set; }
    }
}