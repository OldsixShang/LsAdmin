using Ls.Dto;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto.Sys.RoleManage
{
    /// <summary>
    /// 角色数据传输对象
    /// </summary>
    public class RoleDto : BaseDto
    {
        [Display(Name = "角色名称")]
        [Required(ErrorMessage = "请输入角色名称")]
        public string Name { get; set; }

        //[Display(Name = "描述")]
        //public string Description { get; set; }
    }
}
