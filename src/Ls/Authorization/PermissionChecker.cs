using Ls.Domain.Repositories;
using Ls.IoC;
using Ls.Session;
using System;
using System.Collections.Generic;
using Ls.Caching;
using Ls.Domain.UnitOfWork;

namespace Ls.Authorization
{
    /// <summary>
    /// 权限验证器。
    /// </summary>
    public class PermissionChecker : IPermissionChecker, ITransientDependency
    {

        #region Constants
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : customer role ID
        /// {1} : permission system name
        /// </remarks>
        public const string PERMISSIONS_ALLOWED_KEY = "Ls.permission.allowed-{0}-{1}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        public const string PERMISSIONS_PATTERN_KEY = "Ls.permission.";
        #endregion

        private readonly ICacheManager _cacheManager;
        private readonly IUserStore _userStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="userStore"></param>
        public PermissionChecker(ICacheManager cacheManager,
            IUserStore userStore)
        {
            _cacheManager = cacheManager;
            _userStore = userStore;
        }

        /// <summary>
        /// 用户Session。
        /// </summary>
        public ILsSession LsSession { get; set; }

        /// <summary>
        /// 检查权限。
        /// </summary>
        /// <param name="permission">权限</param>
        public void Check(string permission)
        {
            if (string.IsNullOrEmpty(permission))
            {
                throw new LsException("请提供权限名称", LsExceptionEnum.NoPermission);
            }
            if (!LsSession.UserId.HasValue)
            {
                throw new LsException("请登录。", LsExceptionEnum.NoLogin);
            }
            if (LsSession.UserId == 1)
            {
                return;
            }
            var role = _userStore.Get(LsSession.UserId.Value).Role;
            if (role == null)
            {
                throw new LsException("获取用户角色失败。", LsExceptionEnum.NoRole);
            }
            if (!IsGrant(role, permission))
            {
                throw new LsException(string.Format("用户无操作 {0} 的执行权限。", permission), LsExceptionEnum.NoPermission);
            }
        }

        private bool IsGrant(IRole role, string permissionString)
        {
            string key = string.Format(PERMISSIONS_ALLOWED_KEY, role.Id, permissionString);
            return _cacheManager.Get(key, () =>
            {
                foreach (var permission1 in role.Permissions)
                    if (permission1.Name.Equals(permissionString, StringComparison.InvariantCultureIgnoreCase))
                        return true;

                return false;
            });
        }
    }
}