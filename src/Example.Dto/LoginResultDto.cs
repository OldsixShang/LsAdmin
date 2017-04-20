using Ls.Dto;
using Example.Dto.Sys.UserManage;

namespace Example.Dto
{
    public class LoginResultDto : BaseDto
    {
        /// <summary>
        /// 登录结果
        /// </summary>
        public LoginResult Result { get; set; }
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserDto User { get; set; }
    }

    /// <summary>
    /// 登录结果
    /// </summary>
    public enum LoginResult
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        Success = 100,
        /// <summary>
        /// 用户名不存在
        /// </summary>
        UserNameNotExist = 101,
        /// <summary>
        /// 密码错误
        /// </summary>
        InvalidPassword = 102,
    } 

}
