using Ls.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Ls.Authorization
{
    /// <summary>
    /// 角色。
    /// </summary>
    public interface IRole<TRole, TPermission, TAction, TMenu> : IMultiTenancy, IEntity<Int64>
        where TRole : IRole<TRole, TPermission, TAction, TMenu>
        where TPermission : IPermission<TPermission, TAction, TMenu>
        where TAction : IAction
        where TMenu : IMenu
    {
        /// <summary>
        /// 角色名称。
        /// </summary>
        string Name { get; set; }

        ICollection<TPermission> Permissions { get; set; }

        string Description { get; set; }
    }
}
