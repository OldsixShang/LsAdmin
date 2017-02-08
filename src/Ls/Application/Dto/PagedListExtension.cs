using System.Collections.Generic;
using System.Linq;

namespace Ls.Application.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public static class PagedListExtension
    {
        /// <summary>
        /// 将转IQueryable集合转化成 翻页集合对象
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize)
        {
            return new PagedList<T>(source, pageIndex, pageSize);
        }

        /// <summary>
        ///  将转IEnumerable集合转化成 翻页集合对象
        /// </summary>
        /// <param name="source"></param>
        /// <param name="totalCount"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> source, int totalCount)
        {
            return new PagedList<T>(source, totalCount);
        }
    }
}