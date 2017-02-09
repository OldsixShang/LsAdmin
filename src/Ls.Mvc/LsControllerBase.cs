using Ls.Session;
using System.Web.Mvc;
using Ls.Application;
using Ls.Logging;

namespace Ls.Mvc {
    /// <summary>
    /// Ls Controller 基类。
    /// </summary>
    public abstract class LsControllerBase : Controller {
        /// <summary>
        /// 用户信息 Session。
        /// </summary>
        public ILsSession LsSession { get; set; }

        /// <summary>
        /// 日志记录器。
        /// </summary>
        //public ILogger Log { get; set; }
        //public ILogger Log = null;//LogManager.GetLogger(typeof(LsControllerBase));
        protected ILogger Log {
            get
            {
                return LogManager.GetLogger(this.GetType());;
            }
        }


        /// <summary>
        /// Controller 基类构造函数，初始化 Session。
        /// </summary>
        public LsControllerBase() {

         
            LsSession = NullLsSession.Instance;
        }
    }
}
