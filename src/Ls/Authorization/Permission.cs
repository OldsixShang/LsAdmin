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
    public class Permission : Entity
    {
        private ICollection<Permission> _children;

        /// <summary>
        /// 父级权限。
        /// </summary>
        public virtual Permission Parent { get; set; }

        /// <summary>
        /// 权限名。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 子级权限。
        /// </summary>
        public virtual ICollection<Permission> Children
        {
            get { return _children??(_children=new List<Permission>()); }
            set { _children = value; }
        }

        public long? MenuId { get; set; }
    }
}
