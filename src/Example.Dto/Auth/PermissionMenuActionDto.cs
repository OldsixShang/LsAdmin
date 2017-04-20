using Ls.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dto.Auth
{
    /// <summary>
    /// 权限对象
    /// </summary>
    public class PermissionMenuActionDto : TreeNodeDto<PermissionMenuActionDto>
    {
        public PermissionMenuActionDto()
            :base()
        {
            
        }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        public string MenuType { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool? Checked { get; set; }

    }
}
