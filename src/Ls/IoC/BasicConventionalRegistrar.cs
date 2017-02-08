using Ls.Configutation;
using Castle.DynamicProxy;
using Castle.MicroKernel.Registration;

namespace Ls.IoC {
    /// <summary>
    /// 默认依赖注册器。
    /// </summary>
    public class BasicConventionalRegistrar : IConventionalDependencyRegistrar {
        /// <summary>
        /// 根据上下文注册程序集。
        /// </summary>
        /// <param name="context">注册上下文</param>
        public void RegisterAssembly(IConventionalRegistrationContext context) {
            context.IocManager.IocContainer.Register(
                    Classes.FromAssembly(context.Assembly).IncludeNonPublicTypes().BasedOn<ITransientDependency>().WithService.Self().WithService.DefaultInterfaces().LifestyleTransient(),
                    Classes.FromAssembly(context.Assembly).IncludeNonPublicTypes().BasedOn<ISingletonDependency>().WithService.Self().WithService.DefaultInterfaces().LifestyleSingleton(),
                    Classes.FromAssembly(context.Assembly).IncludeNonPublicTypes().BasedOn<IInterceptor>().WithService.Self().LifestyleTransient()
                );
        }
    }
}
