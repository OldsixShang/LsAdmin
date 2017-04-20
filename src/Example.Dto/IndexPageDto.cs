using Example.Dto.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dto
{
    /// <summary>
    /// 首页页面数据传输对象
    /// </summary>
    public class IndexPageDto
    {
        public IndexPageDto()
        {
            LeftNavMenuList = new List<PermissionMenuDto>();
            TopNavMenuList = new List<PermissionMenuDto>();
            SolidNavMenuList = new List<PermissionMenuDto>();
        }
        /// <summary>
        /// 左侧导航栏权限列表
        /// </summary>
        public List<PermissionMenuDto> LeftNavMenuList { get; set; }
        /// <summary>
        /// 顶部导航栏权限列表
        /// </summary>
        public List<PermissionMenuDto> TopNavMenuList { get; set; }
        /// <summary>
        /// 固定导航栏权限列表
        /// </summary>
        public List<PermissionMenuDto> SolidNavMenuList { get; set; }

    }
}
