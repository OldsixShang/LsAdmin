namespace Ls.Authorization {
    /// <summary>
    /// 权限验证器接口。
    /// </summary>
    public interface IPermissionChecker {
        /// <summary>
        /// 检查权限。
        /// </summary>
        /// <param name="permissionId">权限编号</param>
        /// <param name="requestUri">请求地址</param>
        void Check(long? permissionId,string requestUri);
    }
}
