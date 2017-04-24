using Ls.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Example.Domain.Repositories.Authorization;
using Ls.Extensions;
using System.Data.Entity;
using Example.Domain.Entities.Authorization;
using Example.Repository;
using Ls.Model;

namespace ExampleRepository.Repositories.Authorizations
{
    /// <summary>
    /// 用户仓储实现
    /// </summary>
    public class UserRepository : EfRepository<ExampleDbContext, User>, IUserRepository
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="roleId">角色</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="pager">分页信息</param>
        /// <returns>用户信息</returns>
        public List<User> QueryPager(string name, long? roleId, string realName, Pager pager)
        {
            var query = Context.Users
                .Include(t => t.Role)
                .AsNoTracking().AsQueryable();
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            if (roleId != null) query = query.Where(t => t.RoleId == roleId);
            if (!string.IsNullOrEmpty(realName)) query = query.Where(t => t.RealName.Contains(realName));
            query = query.Page(pager);
            return query.ToList();
        }
    }
}
