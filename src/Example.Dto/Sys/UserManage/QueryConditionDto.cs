using Ls.Dto;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto.Sys.UserManage
{
    public class QueryConditionDto : BaseDto
    {
        [Display(Name = "用户名")]
        public string UserName { get; set; }
        [Display(Name = "真实姓名")]
        public string RealName { get; set; }
        [Display(Name = "角色")]
        public string RoleId { get; set; }
    }
}
