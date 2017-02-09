using Ls.Application;
using Ls.IoC;
using Castle.Core;
using Castle.MicroKernel;

namespace Ls.Authorization.Interceptor {
    /// <summary>
    /// 权限验证注册。
    /// </summary>
    public static class AuthorizationInterceptorRegistrar {
        /// <summary>
        /// 注册权限验证拦截器。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        public static void Initialize(IIocManager iocManager) {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler) {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation)) {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(AuthorizationInterceptor)));
            }
        }
    }
}
