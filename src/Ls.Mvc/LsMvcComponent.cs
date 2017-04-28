using Ls.Components;
using System.Reflection;
using System.Web;
using Ls.Mvc.Fakes;
using Castle.MicroKernel.Registration;
using Ls.Mvc.Validate;
using System.Web.Mvc;

namespace Ls.Mvc {
    /// <summary>
    /// 框架 MVC 组件。
    /// </summary>
    [DependsOn(typeof(LsCoreComponent))]
    public class LsMvcComponent : ComponentBase {
        /// <summary>
        /// 组件预加载。
        /// </summary>
        public override void PreInitialize() {
            IocManager.Register<LsControllerFactory>();
            IocManager.AddConventionalRegistrar(new MvcConventionalRegistrar());
        }

        /// <summary>
        /// 组件加载。
        /// </summary>
        public override void Initialize() {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<IValidation, Validate.Validation>();
            IocManager.Register<IValidationHandler, ValidateHandler>();
            IocManager.Register<IActionInvoker, LsControllerActionInvoker>();
            IocManager.IocContainer.Register(Component.For<HttpContextBase>()
                .LifeStyle.Transient
                .UsingFactoryMethod(() =>  HttpContext.Current != null ?
             (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
             (new FakeHttpContext("~/") as HttpContextBase)));
            // .UsingFactoryMethod(() => new HttpContextWrapper(HttpContext.Current)));
        }
    }
}
