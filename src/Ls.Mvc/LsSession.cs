using System;
using Ls.IoC;
using Ls.Session;
using System.Web;
using System.Web.Security;
using Ls.Authorization;
using Ls.Domain.Repositories;
using Ls.Domain.UnitOfWork;
using TTS.Framework.Extension;

namespace Ls.Mvc {
    /// <summary>
    /// MVC Session 实现。
    /// </summary>
    public class LsSession : ILsSession, ITransientDependency
    {

        private readonly HttpContextBase _context;

        public LsSession( HttpContextBase context)
        {
            _context = context;
        }



        /// <summary>
        /// 请求上下文。
        /// </summary>
       //
       // public HttpContextBase Context { get; set; }

        /// <summary>
        /// 租户编号。
        /// </summary>
        public int? TenantId {
            get { return 0; }
        }

        /// <summary>
        /// 用户编号。
        /// </summary>
        public long? UserId
        {
            get
            {
                long _uid;
                if (!_context.User.Identity.IsAuthenticated) throw new LsException("请先认证!", LsExceptionEnum.NoLogin);
                string userdata = ((FormsIdentity)_context.User.Identity).Ticket.UserData;
                if (!long.TryParse(userdata, out _uid)) throw new LsException("请先认证!", LsExceptionEnum.NoLogin);
                return _uid;
            }
        }

        public void SignIn(IUser user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                string.IsNullOrEmpty(user.UserName)?"匿名用户":user.UserName,
                now,
                now.Add(FormsAuthentication.Timeout),
                createPersistentCookie,
                user.Id.ToString(),
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }
            _context.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }


        public string UserIp
        {
            get { return _context.Request.GetIpAddress(); }
        }
    }
}
