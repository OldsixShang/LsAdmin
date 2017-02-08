namespace Ls.Authorization {
    /// <summary>
    /// 权限验证器接口。
    /// </summary>
    public interface IPermissionChecker {
        /// <summary>
        /// 检查权限。
        /// </summary>
        /// <param name="permission">权限</param>
        void Check(string permission);
    }
}
