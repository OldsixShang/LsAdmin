using System.Collections.Generic;

namespace Ls.Application.Dto
{
    /// <summary>
    /// 翻页容器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// 总行数
        /// </summary>
        int TotalCount { get; set; } 
    }
}