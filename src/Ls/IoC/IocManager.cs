using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ls.IoC {
    /// <summary>
    /// IoC 管理器。
    /// </summary>
    public class IocManager : IIocManager {
        /// <summary>
        /// IoC 管理器对象。
        /// </summary>
        public static IocManager Instance { get; private set; }

        /// <summary>
        /// CastleWindsor 容器。
        /// </summary>
        public IWindsorContainer IocContainer { get; private set; }

        private readonly List<IConventionalDependencyRegistrar> _conventionalRegistrars;

        static IocManager() {
            Instance = new IocManager();
        }

        /// <summary>
        /// IoC 管理器构造函数。
        /// </summary>
        public IocManager() {
            IocContainer = new WindsorContainer();
            _conventionalRegistrars = new List<IConventionalDependencyRegistrar>();
            IocContainer.Register(Component.For<IocManager, IIocManager>().UsingFactoryMethod(() => this));
        }

        /// <summary>
        /// 添加依赖注册器。
        /// </summary>
        /// <param name="registrar">依赖注册器</param>
        public void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar) {
            _conventionalRegistrars.Add(registrar);
        }

        /// <summary>
        /// 注册程序集。
        /// </summary>
        /// <param name="assembly">程序集</param>
        public void RegisterAssemblyByConvention(Assembly assembly) {
            IConventionalRegistrationContext context = new ConventionalRegistrationContext(assembly, this);
            foreach (var registrar in _conventionalRegistrars) {
                registrar.RegisterAssembly(context);
            }
            IocContainer.Install(FromAssembly.Instance(assembly));
        }

        /// <summary>
        /// 注册类型。
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="lifeCycle">对象生命周期</param>
        public void Register<T>(DependencyLifeCycle lifeCycle = DependencyLifeCycle.Singleton) where T : class {
            IocContainer.Register(ApplyLifeCycle(Component.For<T>(), lifeCycle));
        }

        /// <summary>
        /// 注册类型。
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="lifeCycle">对象生命周期</param>
        public void Register(Type type, DependencyLifeCycle lifeCycle = DependencyLifeCycle.Singleton) {
            IocContainer.Register(ApplyLifeCycle(Component.For(type), lifeCycle));
        }

        /// <summary>
        /// 注册类型。
        /// </summary>
        /// <typeparam name="TType">目标类型</typeparam>
        /// <typeparam name="TImpl">实现类型</typeparam>
        /// <param name="lifeCycle">对象生命周期</param>
        public void Register<TType, TImpl>(DependencyLifeCycle lifeCycle)
            where TType : class
            where TImpl : class, TType {
            IocContainer.Register(ApplyLifeCycle(Component.For<TType, TImpl>().ImplementedBy<TImpl>(), lifeCycle));
        }

        /// <summary>
        /// 注册类型。
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="impl">实现类型</param>
        /// <param name="lifeCycle">对象生命周期</param>
        public void Register(Type type, Type impl, DependencyLifeCycle lifeCycle = DependencyLifeCycle.Singleton) {
            IocContainer.Register(ApplyLifeCycle(Component.For(type, impl).ImplementedBy(impl), lifeCycle));
        }

        /// <summary>
        /// 从依赖注入容器解析类型的实例。
        /// </summary>
        /// <typeparam name="T">待解析类型</typeparam>
        /// <returns>类型的实例。</returns>
        public T Resolve<T>() {
            return IocContainer.Resolve<T>();
        }

    

        /// <summary>
        /// 从依赖注入容器解析类型的实例。
        /// </summary>
        /// <param name="type">待解析类型</param>
        /// <returns>类型的实例。</returns>
        public object Resolve(Type type) {
            
            return IocContainer.Resolve(type);
        }

        /// <summary>
        /// 从依赖注入容器中释放对象。
        /// </summary>
        /// <param name="obj">待释放的对象</param>
        public void Release(object obj) {
            IocContainer.Release(obj);
        }

        /// <summary>
        /// 判断类型是否已注册。
        /// </summary>
        /// <param name="type">类型参数</param>
        /// <returns>指示类型是否已注册。</returns>
        public bool IsRegistered(Type type) {
            return IocContainer.Kernel.HasComponent(type);
        }

        private static ComponentRegistration<T> ApplyLifeCycle<T>(ComponentRegistration<T> registration, DependencyLifeCycle lifeCycle)
            where T : class {
            switch (lifeCycle) {
                case DependencyLifeCycle.Transient:
                    return registration.LifestyleTransient();
                case DependencyLifeCycle.Singleton:
                    return registration.LifestyleSingleton();
                default:
                    return registration;
            }
        }

    }
}
