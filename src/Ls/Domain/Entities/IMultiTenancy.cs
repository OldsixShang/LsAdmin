namespace Ls.Domain.Entities {
    /// <summary>
    /// 多租户接口。
    /// </summary>
    public interface IMultiTenancy {
        /// <summary>
        /// 租户编号。
        /// </summary>
        int TenantId { get; set; }
    }
}
