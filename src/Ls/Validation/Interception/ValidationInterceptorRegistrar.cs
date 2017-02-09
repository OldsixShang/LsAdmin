using Ls.Application;
using Ls.IoC;
using Castle.Core;
using Castle.MicroKernel;

namespace Ls.Validation.Interception {
    /// <summary>
    /// 数据验证拦截器注册。
    /// </summary>
    internal static class ValidationInterceptorRegistrar {
        /// <summary>
        /// 注册数据验证拦截器。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        public static void Initialize(IIocManager iocManager) {
            iocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private static void Kernel_ComponentRegistered(string key, IHandler handler) {
            if (typeof(IApplicationService).IsAssignableFrom(handler.ComponentModel.Implementation)) {
                handler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ValidationInterceptor)));
            }
        }
    }
}
