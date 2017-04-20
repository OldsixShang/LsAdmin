using Ls.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using Ls.Extensions;
using Example.Repository;
using Ls.Model;

namespace ExampleRepository.Repositories.Authorizations
{
    /// <summary>
    /// 菜单仓储实现
    /// </summary>
    public class MenuRepository : EfRepository<PlatformDbContext, Menu>, IMenuRepository
    {
        /// <summary>
        /// 分页查询菜单信息
        /// </summary>
        /// <param name="name">菜单名称</param>
        /// <param name="pager">分页信息</param>
        /// <returns>菜单信息</returns>
        public List<Menu> QueryPager(string name, Pager pager)
        {
            var query = Context.Menus.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            query = query.Page(pager);
            return query.ToList();
        }

        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <param name="name">菜单名称</param>
        /// <returns>菜单信息列表</returns>
        public List<Menu> Query(string name)
        {
            var query = Context.Menus.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            return query.ToList();
        }
    }
}
