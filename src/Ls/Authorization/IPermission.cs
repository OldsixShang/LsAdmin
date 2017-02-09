using Ls.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Authorization {
    /// <summary>
    /// 权限。
    /// </summary>
    public interface IPermission : IEntity<Int64>
    {
        /// <summary>
        /// 父级权限。
        /// </summary>
        IPermission Parent { get; set; }
        /// <summary>
        /// 权限名。
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 子级权限。
        /// </summary>
        ICollection<IPermission> Children { get; set; }
        
    }
}
