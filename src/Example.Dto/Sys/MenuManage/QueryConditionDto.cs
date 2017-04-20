using Ls.Dto;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto.Sys.MenuManage
{
    public class QueryConditionDto : BaseDto
    {
        [Display(Name = "菜单名称")]
        public string Name { get; set; }

        [Display(Name = "菜单类型")]
        public int? MenuType { get; set; }
    }
}
