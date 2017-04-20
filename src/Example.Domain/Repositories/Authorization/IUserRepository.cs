using Ls.Domain.Repositories;
using Ls.Model;
using System.Collections.Generic;
using Example.Domain.Entities.Authorization;

namespace Example.Domain.Repositories.Authorization
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    public interface IUserRepository :IRepository<User>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="name">用户名</param>
        /// <param name="roleId">角色</param>
        /// <param name="realName">真实姓名</param>
        /// <param name="pager">分页信息</param>
        /// <returns>用户信息</returns>
        List<User> QueryPager(string name, long? roleId, string realName, Pager pager);
    }
}
