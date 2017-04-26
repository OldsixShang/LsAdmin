using Ls.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Ls.Authorization
{
    /// <summary>
    /// 角色。
    /// </summary>
    public interface IRole: IMultiTenancy, IEntity<string>
    {
        /// <summary>
        /// 角色名称。
        /// </summary>
        string Name { get; set; }
        string Description { get; set; }
    }

    /// <summary>
    /// 角色。
    /// </summary>
    public interface IRole<TRole, TPermission, TAction, TMenu> : IRole
        where TRole : IRole<TRole, TPermission, TAction, TMenu>
        where TPermission : IPermission<TPermission, TAction, TMenu>
        where TAction : IAction
        where TMenu : IMenu
    {

        ICollection<TPermission> Permissions { get; set; }
    }
}
