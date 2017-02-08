using Ls.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Ls.Authorization {
    /// <summary>
    /// 角色。
    /// </summary>
    public class Role : SoftDeleteEntity, IMultiTenancy
    {
        private ICollection<Permission> _permissions;


        /// <summary>
        /// 角色名称。
        /// </summary>
       public  string Name { get; set; }

        /// <summary>
        /// 角色对应的权限。
        /// </summary>
        public virtual ICollection<Permission> Permissions
        {
            get { return _permissions ?? (_permissions=new List<Permission>()); }
            set { _permissions = value; }
        }


        public int TenantId{ get; set; }
    }
}
