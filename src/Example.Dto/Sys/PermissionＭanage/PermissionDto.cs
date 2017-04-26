using Ls.Dto;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto.Sys.PermissionManage
{
    /// <summary>
    /// 权限数据传输对象
    /// </summary>
    public class PermissionDto : BaseDto
    {
        [Display(Name = "权限名称")]
        [Required(ErrorMessage = "请输入权限名称")]
        public string Name { get; set; }
        /// <summary>
        /// 父级权限id
        /// </summary>
        [Display(Name = "父级权限")]
        [JsonProperty("_parentId")]
        public string ParentId { get; set; }

        #region 菜单相关
        /// <summary>
        /// 菜单Id
        /// </summary>
        [Display(Name="菜单")]
        public string MenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 菜单地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 菜单类型
        /// </summary>
        public string MenuType { get; set; }
        #endregion

        #region 操作相关
        /// <summary>
        /// 操作Id
        /// </summary>
        [Display(Name = "操作")]
        public string ActionId { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        public string ActionName { get; set; }
        /// <summary>
        /// Html模板
        /// </summary>
        public string Template { get; set; } 
        #endregion
    }
}
