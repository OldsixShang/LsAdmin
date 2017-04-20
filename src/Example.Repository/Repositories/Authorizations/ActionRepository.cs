using Ls.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using Example.Repository;
using Ls.Extensions;
using Ls.Model;

namespace ExampleRepository.Repositories.Authorizations
{
    /// <summary>
    /// 系统操作仓储实现
    /// </summary>
    public class ActionRepository : EfRepository<PlatformDbContext, AuthAction>, IActionRepository
    {
        /// <summary>
        /// 分页查询系统操作信息
        /// </summary>
        /// <param name="name">系统操作名称</param>
        /// <param name="pager">分页信息</param>
        /// <returns>系统操作信息</returns>
        public List<AuthAction> QueryPager(string name, Pager pager)
        {
            var query = Context.Actions.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            query = query.Page(pager);
            return query.ToList();
        }

        /// <summary>
        /// 查询系统操作信息
        /// </summary>
        /// <param name="name">系统操作名称</param>
        /// <returns>系统操作信息列表</returns>
        public List<AuthAction> Query(string name)
        {
            var query = Context.Actions.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            return query.ToList();
        }
    }
}
