using Ls.Domain.Repositories;
using Ls.Model;
using System.Collections.Generic;
using Example.Domain.Entities.Authorization;

namespace Example.Domain.Repositories.Authorization
{
    /// <summary>
    /// 菜单仓储接口
    /// </summary>
    public interface IMenuRepository :IRepository<Menu>
    {
        /// <summary>
        /// 分页查询菜单信息
        /// </summary>
        /// <param name="name">菜单名称</param>
        /// <param name="pager">分页信息</param>
        /// <returns>菜单信息</returns>
        List<Menu> QueryPager(string name, Pager pager);
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <param name="name">菜单名称</param>
        /// <returns>菜单信息列表</returns>
        List<Menu> Query(string name);
    }
}
