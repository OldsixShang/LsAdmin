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
        public const string PERMISSIONS_PATTERN_KEY = "Ls.permission";

        public void Check(long? permissionId, string requestUri)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}