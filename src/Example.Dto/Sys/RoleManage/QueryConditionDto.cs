using Ls.Dto;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto.Sys.RoleManage
{
    public class QueryConditionDto : BaseDto
    {
        [Display(Name = "角色名称")]
        public string RoleName { get; set; }
    }
}
