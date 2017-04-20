using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dto
{
    public class ModifyPasswordDto
    {
        [Display(Name="原始密码")]
        [Required(ErrorMessage="请输入原始密码！")]
        public string OldPassword { get; set; }
        [Display(Name = "新密码")]
        [Required(ErrorMessage = "请输入新密码！")]
        public string NewPassword { get; set; }

        [Display(Name = "确认密码")]
        [Required(ErrorMessage = "请输入再输入一次新密码！")]
        [Compare("NewPassword",ErrorMessage = "两次密码输入不一致")]
        public string ConfirmPassword { get; set; }

    }
}
