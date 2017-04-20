using Ls.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dto.Sys.ActionManage
{
    public class QueryConditionDto : BaseDto
    {
        [Display(Name = "操作名称")]
        public string ActionName { get; set; }
    }
}
