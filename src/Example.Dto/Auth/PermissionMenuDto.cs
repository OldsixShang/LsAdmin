using Ls.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dto.Auth
{
    /// <summary>
    /// Mvc数据传输对象
    /// </summary>
    public class PermissionMenuDto : TreeNodeDto<PermissionMenuDto>
    {
        public PermissionMenuDto() : base() {

        }
        [JsonProperty("_parentId")]
        public override string ParentId
        {
            get
            {
                return base.ParentId;
            }

            set
            {
                base.ParentId = value;
            }
        }
        [JsonProperty("children")]
        public override List<PermissionMenuDto> Children
        {
            get
            {
                return base.Children;
            }

            set
            {
                base.Children = value;
            }
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }

    }
}
