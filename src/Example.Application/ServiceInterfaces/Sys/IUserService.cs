using System.Collections.Generic;
using Ls.Model;
using Example.Dto.Sys.UserManage;
using Example.Dto;

namespace Example.Application.ServiceInterfaces.Sys
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto">登录对象</param>
        /// <returns>登录结果</returns>
        LoginResultDto Login(LoginDto dto);
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="Id">用户唯一标识</param>
        /// <returns>用户信息</returns>
        UserDto GetUser(string Id);
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto">用户信息</param>
        void AddUser(UserDto dto);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="dto">用户信息</param>
        void DeleteUser(UserDto dto);
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="dto">用户信息</param>
        void ModifyUser(UserDto dto);

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>用户信息</returns>
        IList<UserDto> QueryUser(QueryConditionDto conditionDto);
        /// <summary>
        /// 分页查询用户信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>用户信息</returns>
        IList<UserDto> QueryPagerUser(QueryConditionDto conditionDto,Pager pager);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto">密码修改数据传输对象</param>
        void ModifyPasswordDto(ModifyPasswordDto dto);
    }
}
