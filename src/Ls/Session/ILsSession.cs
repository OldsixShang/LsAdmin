using Ls.Authorization;
using Ls.IoC;

namespace Ls.Session {
    /// <summary>
    /// Ls 框架 Session 接口。
    /// </summary>
    public interface ILsSession
    {


        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="user"></param>
        /// <param name="createPersistentCookie"></param>
        void SignIn(IUser user, bool createPersistentCookie);

        /// <summary>
        /// 退出
        /// </summary>
        void SignOut();

        /// <summary>
        /// 租户编号。
        /// </summary>
        int? TenantId { get; }

        /// <summary>
        /// 用户编号。
        /// </summary>
        long? UserId { get; }

        /// <summary>
        /// 用户Ip地址
        /// </summary>
        string UserIp { get; }
    }
}
