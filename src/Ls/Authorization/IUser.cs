using System;
using Ls.Domain.Entities;

namespace Ls.Authorization
{
    /// <summary>
    /// 用户。
    /// </summary>
    public interface IUser : IEntity, ISoftDelete, IMultiTenancy, ICreatedTime, ILastUpdateTime
    {
        /// <summary>
        /// 用户名。
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// 登录名。
        /// </summary>
        string LoginId { get; set; }

        /// <summary>
        /// 密码。
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        long? RoleId { get; set; }
        
    }
}
