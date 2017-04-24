using Ls;
using Ls.Authorization;
using Ls.Domain.Entities;
using Ls.Utilities;
using System;

namespace Example.Domain.Entities.Authorization
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : SoftDeleteEntity, IUser<User,Role,Permission,AuthAction,Menu>
    {
        #region 属性
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 工号。
        /// </summary>
        public string LoginId { get; set; }
        /// <summary>
        /// 密码。
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public byte[] Icon { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime? LoginTime { get; set; }
        /// <summary>
        /// 角色编号。
        /// </summary>
        public virtual long? RoleId { get; set; }
        /// <summary>
        /// 角色。
        /// </summary>
        public virtual Role Role { get; set; }
        /// <summary>
        /// 租户编号。
        /// </summary>
        public int TenantId { get; set; }
        /// <summary>
        /// 失败次数
        /// </summary>
        public int FailuerCounts { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime LastUpdatedTime { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 验证密码 
        /// </summary>
        /// <param name="unEncryptedPassword">明文密码串</param>
        /// <param name="encrypt">是否加密</param>
        /// <returns>验证结果</returns>
        public bool CheckPassword(string unEncryptedPassword,bool encrypt = false)
        {
            return this.Password.Equals(encrypt ? MD5Util.GetMD5_16(unEncryptedPassword) : unEncryptedPassword);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        public User ModifyPassword(string newUnencryptedPass)
        {
            if (Password.Equals(newUnencryptedPass))
                throw new LsException("新密码与原始密码不能相同");
            Password = newUnencryptedPass;
            return this;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        public User ReSetPassword(bool encrypt = false)
        {
            return InitPassword(encrypt);
        }
        /// <summary>
        /// 初始化密码
        /// </summary>
        public User InitPassword(bool encrypt = false)
        {
            string initialPassword = "123456";
            Password =encrypt?MD5Util.GetMD5_16(initialPassword):initialPassword;
            return this;
        }
        #endregion
    }
}
