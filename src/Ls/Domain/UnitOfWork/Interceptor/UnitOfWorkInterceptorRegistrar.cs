using Ls.Application;
using Ls.IoC;
using Castle.Core;
using Castle.MicroKernel;

namespace Ls.Domain.UnitOfWork.Interceptor {
    /// <summary>
    /// 工作单元注册。
    /// </summary>
    internal static class UnitOfWorkInterceptorRegistrar {
        /// <summary>
        /// 注册工作单元拦截器。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        public static void Initialize(IIocManager iocManager) {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler) {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation)) {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(UnitOfWorkInterceptor)));
            }
        }
    }
}
