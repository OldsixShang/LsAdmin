using Ls.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Example.Domain.Repositories.Authorization;
using Ls.Extensions;
using System.Data.Entity;
using Ls.Model;
using Example.Repository;
using Example.Domain.Entities.Authorization;

namespace ExampleRepository.Repositories.Authorizations
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class RoleRepository : EfRepository<PlatformDbContext, Role>, IRoleRepository
    {
        /// <summary>
        /// 分页查询角色信息
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="pager">分页信息</param>
        /// <returns>角色信息</returns>
        public List<Role> QueryPager(string name, Pager pager)
        {
            var query = Context.Roles.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            query = query.Page(pager);
            return query.ToList();
        }

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <returns>角色信息列表</returns>
        public List<Role> Query(string name)
        {
            var query = Context.Roles.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            return query.ToList();
        }
        /// <summary>
        /// 异步查询角色信息列表
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="remoteCount">异步查询数量</param>
        /// <returns>角色信息</returns>
        public List<Role> QueryRemote(string name, int remoteCount = 20)
        {
            var query = Context.Roles.AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            query = query.Take(remoteCount);
            return query.ToList();
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Role GetRole(long id)
        {
            return Context.Roles.Include(t => t.Permissions).Where(t => t.Id == id).FirstOrDefault();
        }
    }
}
