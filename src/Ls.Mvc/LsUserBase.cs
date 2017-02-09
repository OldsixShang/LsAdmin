namespace Ls.Mvc {
    /// <summary>
    /// 用户登录信息基类。
    /// </summary>
    public abstract class LsUserBase {
        public int? TenantId { get; set; }
        public int? UserId { get; set; }
    }
}
