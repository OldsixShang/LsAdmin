using Ls.Authorization.Interceptor;
using Ls.Components;
using Ls.Domain.UnitOfWork.Interceptor;
using Ls.IoC;
using Ls.Validation.Interception;
using System.Reflection;
using Ls.Authorization;
using Ls.Caching;
using Castle.MicroKernel.Registration;

namespace Ls {
    /// <summary>
    /// 框架核心组件，随便框架一同初始化。
    /// </summary>
    public class LsCoreComponent : ComponentBase {
        /// <summary>
        /// 核心组件构造器。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        public LsCoreComponent(IIocManager iocManager) {
            IocManager = iocManager;
        }

        /// <summary>
        /// 组件初始化前操作。
        /// </summary>
        public override void PreInitialize() {
            IocManager.AddConventionalRegistrar(new BasicConventionalRegistrar());
            ValidationInterceptorRegistrar.Initialize(IocManager);
            UnitOfWorkInterceptorRegistrar.Initialize(IocManager);
            AuthorizationInterceptorRegistrar.Initialize(IocManager);
        }

        /// <summary>
        /// 组件初始化。
        /// </summary>
        public override void Initialize() {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<ICacheManager, MemoryCacheManager>(DependencyLifeCycle.Singleton);
            //IocManager.Register<User>(DependencyLifeCycle.Transient);
        }
    }
}
