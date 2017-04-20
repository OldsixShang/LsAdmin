using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using Example.Dto;
using Ls;
using Ls.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tts.Platform.Application.ServiceInterfaces.Sys;
using UserManage = Example.Dto.Sys.UserManage;
using RoleManage = Example.Dto.Sys.RoleManage;
using Ls.Model;

namespace Tts.Platform.Application.ServiceImplements.Sys
{
    /// <summary>
    /// 用户领域服务
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        #region 字段
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        #endregion

        public UserService(IUserRepository userRepository,
            IRoleRepository roleRepository
            )
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id">用户唯一标识</param>
        /// <returns>用户信息</returns>
        public Example.Dto.Sys.UserManage.UserDto GetUser(long Id)
        {
            User entity = _userRepository.Get(Id);
            return entity.ToDto<UserManage.UserDto>();
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="dto">传入用户信息</param>
        public void AddUser(UserManage.UserDto dto)
        {
            #region 业务验证
            User user = _userRepository.Get(t => t.LoginId == dto.UserName);
            if (user != null) throw new LsException(string.Format("用户名[{0}]已经存在,请确认！", dto.UserName));
            #endregion

            User entity = dto.ToEntity<User>();
            entity.Role = _roleRepository.Get((long)dto.RoleId);
            entity.InitPassword();
            _userRepository.Add(entity);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="dto">传入用户信息</param>
        public void DeleteUser(UserManage.UserDto dto)
        {
            User entity = _userRepository.Get(SafeConvert.ToInt64(dto.Id));
            _userRepository.Delete(entity);
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="dto">传入用户信息</param>
        public void ModifyUser(UserManage.UserDto dto)
        {
            User entity = _userRepository.Get(SafeConvert.ToInt64(dto.Id));
            entity.Email = dto.Email;
            entity.RealName = dto.RealName;
            entity.Phone = dto.Phone;
            entity.RoleId = dto.RoleId;
            _userRepository.Update(entity);
        }
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>用户信息</returns>
        public IList<UserManage.UserDto> QueryUser(UserManage.QueryConditionDto conditionDto)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 分页查询用户信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>分页用户信息</returns>
        public IList<UserManage.UserDto> QueryPagerUser(UserManage.QueryConditionDto conditionDto, Pager pager)
        {
            var entities = _userRepository.QueryPager(conditionDto.UserName, conditionDto.RoleId, conditionDto.RealName, pager);
            return entities.ToListDto<User, UserManage.UserDto>();
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="dto">登录数据传输对象</param>
        /// <returns>登录结果</returns>
        public LoginResultDto Login(LoginDto dto)
        {
            LoginResultDto result = new LoginResultDto { User = new UserManage.UserDto { UserName = dto.UserName }, Result = LoginResult.UserNameNotExist };
            var user = _userRepository.Get(t => t.LoginId == dto.UserName);
            if (user == null) return result;
            if (!user.CheckPassword(dto.Password))
                result.Result = LoginResult.InvalidPassword;
            else
            {
                result.Result = LoginResult.Success;
                LsSession.SignIn(user, true);
            }
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="dto">密码修改数据传输对象</param>
        public void ModifyPasswordDto(ModifyPasswordDto dto)
        {
            var user = _userRepository.Get(LsSession.UserId.Value);
            if (!user.CheckPassword(dto.OldPassword)) throw new LsException("原密码输入不正确");
            user.ModifyPassword(dto.NewPassword);
            _userRepository.Update(user);
        }
    }
}
