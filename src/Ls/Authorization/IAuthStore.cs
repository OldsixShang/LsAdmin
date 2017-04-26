using Ls.Authorization;
using Ls.Domain.Repositories;
using Ls.IoC;
using System.Collections.Generic;

namespace Ls.Authorization
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IAuthStore : IRepository<IUser>, IRepository, ITransientDependency
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IUser GetUser(string userId);
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        IRole GetRole(string roleId);
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        ICollection<IPermission> GetPermissions(string roleId);
    }
}