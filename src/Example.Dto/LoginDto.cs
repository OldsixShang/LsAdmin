using Ls.Dto;
using System.ComponentModel.DataAnnotations;

namespace Example.Dto
{
    public class LoginDto : BaseDto
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "请输入用户名")]
        public string UserName { get; set; }
        [Display(Name = "密码")]
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public string RememberMe { get; set; }
    }
}
