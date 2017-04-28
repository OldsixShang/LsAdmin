using Ls.Session;
using System.Web.Mvc;
using Ls.Application;
using Ls.Logging;
using Ls.IoC;

namespace Ls.Mvc {
    /// <summary>
    /// Ls Controller 基类。
    /// </summary>
    public abstract class LsControllerBase : Controller {
        /// <summary>
        /// 用户信息 Session。
        /// </summary>
        public ILsSession LsSession { get; set; }
        
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
            base.ActionInvoker = IocManager.Instance.Resolve<IActionInvoker>();
        }


    }
}
