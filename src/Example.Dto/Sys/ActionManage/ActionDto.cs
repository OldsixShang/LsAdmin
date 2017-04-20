using Ls.Dto;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto.Sys.ActionManage
{
    /// <summary>
    /// 操作数据传输对象
    /// </summary>
    public class ActionDto : BaseDto
    {
        [Display(Name = "操作名称")]
        [Required(ErrorMessage="请输入操作名称！")]
        public string Name { get; set; }

        [Display(Name = "Html模板")]
        public string Template { get; set; }

        [Display(Name = "描述")]
        public string Description { get; set; }
    }
}
