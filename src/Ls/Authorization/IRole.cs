using Ls.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Ls.Authorization {
    /// <summary>
    /// 角色。
    /// </summary>
    public interface IRole : IMultiTenancy,IEntity<Int64>
    {
        /// <summary>
        /// 角色名称。
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 角色对应的权限。
        /// </summary>
        ICollection<IPermission> Permissions { get; set; }
        
    }
}
