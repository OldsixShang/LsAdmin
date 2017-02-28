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
        
        ICollection<IPermission> Permissions { get; set; }

        string Description { get;set; }
    }
}
