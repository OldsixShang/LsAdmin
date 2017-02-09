﻿namespace Ls.Authorization {
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

        /// <summary>
        /// 检查权限。
        /// </summary>
        /// <param name="permission">权限</param>
        public void Check(string permission) {

        }
    }
}
