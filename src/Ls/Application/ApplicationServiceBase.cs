using Ls.Authorization;
using Ls.Domain.UnitOfWork;
using Ls.IoC;
using Ls.Logging;
using Ls.Session;


namespace Ls.Application {
    /// <summary>
    /// 应用层服务基类。
    /// </summary>
    public abstract class ApplicationServiceBase : IApplicationService, ITransientDependency {
        /// <summary>
        /// 日志记录器。
        /// </summary>
        public ILogger Log {
            get
            {
                return LogManager.GetLogger(this.GetType());
            }
        }

        /// <summary>
        /// Session，记录用户的租户编号、用户编号。
        /// </summary>
        public ILsSession LsSession { get; set; }

        /// <summary>
        /// 工作单元提供者。
        /// </summary>
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }

        /// <summary>
        /// 权限验证器。
        /// </summary>
        public IPermissionChecker PermissionChecker { get; set; }

        /// <summary>
        /// 应用层服务基类构造函数。
        /// </summary>
        public ApplicationServiceBase() {

           // Log = NullLog.Instance;
            LsSession = NullLsSession.Instance;
            PermissionChecker = NullPermissionChecker.Instance;
        }
    }
}
