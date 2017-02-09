using Ls.IoC;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ls.Mvc {
    /// <summary>
    /// Ls 控制器工厂。
    /// </summary>
    public class LsControllerFactory : DefaultControllerFactory {
        private IIocManager _iocManager;

        /// <summary>
        /// 根据 IoC 管理器创建控制器工厂。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        public LsControllerFactory(IIocManager iocManager) {
            _iocManager = iocManager;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType) {
            if (controllerType == null) {
                return null;
            }
            return (IController)_iocManager.Resolve(controllerType);
        }

        public override void ReleaseController(IController controller) {
            _iocManager.Release(controller);
        }
    }
}
