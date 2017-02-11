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
        /// 角色对应权限列表缓存键值
        /// </summary>
        /// <remarks>
        /// {0} : 角色编号
        /// </remarks>
        public const string ROLE_PERMISSIONS_KEY = "Ls.role.permission-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        public const string PERMISSIONS_PATTERN_KEY = "Ls.permission.";
        #endregion

        private readonly ICacheManager _cacheManager;
        private readonly IAuthStore _authStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="authStore"></param>
        public PermissionChecker(ICacheManager cacheManager,
            IAuthStore authStore)
        {
            _cacheManager = cacheManager;
            _authStore = authStore;
        }

        /// <summary>
        /// 用户Session。
        /// </summary>
        public ILsSession LsSession { get; set; }

        /// <summary>
        /// 检查权限。
        /// </summary>
        /// <param name="permissionId">权限编号</param>
        /// <param name="requestUri">请求地址</param>
        public void Check(long? permissionId,string requestUri)
        {
            if (string.IsNullOrEmpty(requestUri))
            {
                throw new LsException("无效的请求", LsExceptionEnum.BusinessException);
            }
            if (permissionId == null)
            {
                throw new LsException("请提供权限编号", LsExceptionEnum.NoPermission);
            }
            if (!LsSession.UserId.HasValue)
            {
                throw new LsException("请登录。", LsExceptionEnum.NoLogin);
            }
            //超级用户
            //if (LsSession.UserId == 1)
            //{
            //    return;
            //}
            var role = _authStore.GetRole(LsSession.RoleId);
            if (role == null)
            {
                throw new LsException("获取用户角色失败。", LsExceptionEnum.NoRole);
            }
            if (!IsGrant(permissionId, requestUri))
            {
                throw new LsException(string.Format("用户无操作 {0} 的执行权限。", permissionId), LsExceptionEnum.NoPermission);
            }
        }

        private bool IsGrant(long? permissionId, string requestUri)
        {
            string key = string.Format(ROLE_PERMISSIONS_KEY, LsSession.RoleId);
            var rolePermissions= _cacheManager.Get(key, () =>
            {
                return _authStore.GetPermissions(LsSession.RoleId);
            });
            foreach(var per in rolePermissions)
            {
                if (per.Id == permissionId && per.Uri == requestUri)
                    return true;
            }
            return false;
        }
    }
}