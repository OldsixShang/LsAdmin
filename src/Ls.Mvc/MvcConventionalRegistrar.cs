using System.Web;
using Ls.IoC;
using Castle.MicroKernel.Registration;
using System.Web.Mvc;

namespace Ls.Mvc {
    /// <summary>
    /// MVC 依赖注册器。
    /// </summary>
    public class MvcConventionalRegistrar : IConventionalDependencyRegistrar {
        /// <summary>
        /// 根据上下文注册程序集。
        /// </summary>
        /// <param name="context">注册上下文</param>
        public void RegisterAssembly(IConventionalRegistrationContext context) {
            context.IocManager.IocContainer.Register(
                Classes.FromAssembly(context.Assembly).IncludeNonPublicTypes().BasedOn<Controller>().WithService.Self().WithService.DefaultInterfaces().LifestyleTransient()
            );
        }

        
    }
}
