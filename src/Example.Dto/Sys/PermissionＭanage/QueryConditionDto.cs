using Ls.Dto;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto.Sys.PermissionManage
{
    public class QueryConditionDto : BaseDto
    {
        [Display(Name = "权限名称")]
        public string PermissionName { get; set; }
    }
}
