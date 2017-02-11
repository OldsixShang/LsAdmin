using System;

namespace Ls.Session {
    /// <summary>
    /// 默认的空 Session。
    /// </summary>
    public class NullLsSession : ILsSession {
        private static readonly NullLsSession instance = new NullLsSession();

        /// <summary>
        /// 空 Session 的单例。
        /// </summary>
        public static NullLsSession Instance { get { return instance; } }

        private NullLsSession() { }

        /// <summary>
        /// 空租户编号。
        /// </summary>
        public int? TenantId { get { return null; } }

        /// <summary>
        /// 空用户编号。
        /// </summary>
        public long? UserId { get { return null; } }

      

        public void SignIn(Authorization.IUser user, bool createPersistentCookie)
        {
            throw new System.NotImplementedException();
        }

        public void SignOut()
        {
            throw new System.NotImplementedException();
        }


        public string UserIp
        {
            get { return null; }
        }

        public long? RoleId
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
