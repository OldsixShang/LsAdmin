using System.Collections.Generic;
using System.Linq;

namespace Ls.Application.Dto
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedList<T> : List<T>, IPagedList<T> 
    {

        public int TotalCount { get; set; }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="source"></param>
        /// <param name="totalCount"></param>
        public PagedList(IEnumerable<T> source, int totalCount)
        {
            TotalCount = totalCount;
            this.AddRange(source);
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize)
        {
            TotalCount = source.Count();
            this.AddRange(source.Skip((pageIndex - 1)*pageSize).Take(pageSize).ToList());

        }
    }
}