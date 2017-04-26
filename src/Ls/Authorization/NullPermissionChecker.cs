using System;

namespace Ls.Authorization {
    /// <summary>
    /// 空权限验证器。
    /// </summary>
    public class NullPermissionChecker : IPermissionChecker {
        private static readonly NullPermissionChecker instance = new NullPermissionChecker();

        /// <summary>
        /// 空权限验证器的单例。
        /// </summary>
        public static NullPermissionChecker Instance { get { return instance; } }

        private NullPermissionChecker() { }
        
        public void Check(string permissionId, string requestUri)
        {
            throw new NotImplementedException();
        }
    }
}
