using System;
using Ls.Domain.Entities;

namespace Ls.Authorization
{
    public interface IUser:IEntity, ISoftDelete, IMultiTenancy, ICreatedTime, ILastUpdatedTime
    {
        /// <summary>
        /// 用户名。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 登录名。
        /// </summary>
        string LoginId { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        string Password { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        long? RoleId { get; set; }

    }
    /// <summary>
    /// 用户。
    /// </summary>
    public interface IUser<TUser,TRole,TPermission,TAction,TMenu> : IUser
        where TUser:IUser<TUser,TRole, TPermission, TAction, TMenu>
        where TRole:IRole<TRole,TPermission, TAction, TMenu>
        where TPermission : IPermission<TPermission, TAction, TMenu>
        where TAction : IAction
        where TMenu : IMenu
    {
        /// <summary>
        /// 角色
        /// </summary>
        TRole Role { get; set; }
    }
}
