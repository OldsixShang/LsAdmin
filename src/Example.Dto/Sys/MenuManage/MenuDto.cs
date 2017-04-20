using Ls.Dto;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto.Sys.MenuManage
{
    /// <summary>
    /// 角色数据传输对象
    /// </summary>
    public class MenuDto : BaseDto
    {
        [Display(Name = "菜单名称")]
        [Required(ErrorMessage="请输入菜单名称")]
        public string Name { get; set; }
        [Display(Name = "菜单类型")]
        public string MenuType { get; set; }

        [Display(Name = "菜单地址")]
        public string Url { get; set; }

        [Display(Name = "菜单图标")]
        public string Icon { get; set; }


    }
}
