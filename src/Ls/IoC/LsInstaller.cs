using Ls.Authorization;
using Ls.Components;
using Ls.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Ls.IoC {
    /// <summary>
    /// 框架核心对象注入器。
    /// </summary>
    internal class LsInstaller : IWindsorInstaller {
        /// <summary>
        /// 执行对象注入。
        /// </summary>
        /// <param name="container">对象注入的容器。</param>
        /// <param name="store"></param>
        public void Install(IWindsorContainer container, IConfigurationStore store) {
            container.Register(
                Component.For<IAssemblyFinder>().ImplementedBy<CurrentDomainAssemblyFinder>().LifestyleSingleton(),
                Component.For<ITypeFinder>().ImplementedBy<TypeFinder>().LifestyleSingleton(),
                Component.For<IComponentFinder>().ImplementedBy<ComponentFinder>().LifestyleSingleton(),
                Component.For<IComponentManager>().ImplementedBy<ComponentManager>().LifestyleSingleton());
        }
    }
}
