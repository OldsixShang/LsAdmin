using Ls.Domain.Repositories;
using System.Collections.Generic;

namespace Ls.Authorization
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public interface IAuthStore<TUser, TRole, TPermission, TAction, TMenu>
        where TUser : IUser<TUser, TRole, TPermission, TAction, TMenu>
        where TRole : IRole<TRole, TPermission, TAction, TMenu>
        where TPermission : IPermission<TPermission, TAction, TMenu>
        where TAction : IAction
        where TMenu : IMenu
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        TUser GetUser(long? userId);
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        TRole GetRole(long? roleId);
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        ICollection<TPermission> GetPermissions(long? roleId);
    }
}