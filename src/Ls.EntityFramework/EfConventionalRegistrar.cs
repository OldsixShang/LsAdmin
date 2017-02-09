using Ls.IoC;
using Castle.MicroKernel.Registration;
using System.Data.Entity;

namespace Ls.EntityFramework {
    /// <summary>
    /// EF 通用组件注册器。
    /// </summary>
    public class EfConventionalRegistrar : IConventionalDependencyRegistrar {
        public void RegisterAssembly(IConventionalRegistrationContext context) {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly).IncludeNonPublicTypes().BasedOn<DbContext>().WithService.Self().WithService.DefaultInterfaces().LifestyleTransient()
                );
        }
    }
}
