using Castle.Windsor;
using System;
using System.Reflection;

namespace Ls.IoC {
    /// <summary>
    /// IoC 管理器接口。
    /// </summary>
    public interface IIocManager {
        /// <summary>
        /// CastleWindsor 容器。
        /// </summary>
        IWindsorContainer IocContainer { get; }

        #region Register

        /// <summary>
        /// 添加依赖注册器。
        /// </summary>
        /// <param name="registrar">依赖注册器</param>
        void AddConventionalRegistrar(IConventionalDependencyRegistrar registrar);

        /// <summary>
        /// 注册程序集。
        /// </summary>
        /// <param name="assembly">程序集</param>
        void RegisterAssemblyByConvention(Assembly assembly);

        /// <summary>
        /// 注册类型。
        /// </summary>
        /// <typeparam name="T">类型参数</typeparam>
        /// <param name="lifeCycle">对象生命周期</param>
        void Register<T>(DependencyLifeCycle lifeCycle = DependencyLifeCycle.Singleton)
            where T : class;

        /// <summary>
        /// 注册类型。
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="lifeCycle">对象生命周期</param>
        void Register(Type type, DependencyLifeCycle lifeCycle = DependencyLifeCycle.Singleton);

        /// <summary>
        /// 注册类型。
        /// </summary>
        /// <typeparam name="TType">目标类型</typeparam>
        /// <typeparam name="TImpl">实现类型</typeparam>
        /// <param name="lifeCycle">对象生命周期</param>
        void Register<TType, TImpl>(DependencyLifeCycle lifeCycle = DependencyLifeCycle.Singleton)
            where TType : class
            where TImpl : class, TType;

        /// <summary>
        /// 注册类型。
        /// </summary>
        /// <param name="type">目标类型</param>
        /// <param name="impl">实现类型</param>
        /// <param name="lifeCycle">对象生命周期</param>
        void Register(Type type, Type impl, DependencyLifeCycle lifeCycle = DependencyLifeCycle.Singleton);

        #endregion

        #region Resolve

        /// <summary>
        /// 从依赖注入容器解析类型的实例。
        /// </summary>
        /// <typeparam name="T">待解析类型</typeparam>
        /// <returns>类型的实例。</returns>
        T Resolve<T>();


        /// <summary>
        /// 从依赖注入容器解析类型的实例。
        /// </summary>
        /// <param name="type">待解析类型</param>
        /// <returns>类型的实例。</returns>
        object Resolve(Type type);

        #endregion

        /// <summary>
        /// 从依赖注入容器中释放对象。
        /// </summary>
        /// <param name="obj">待释放的对象</param>
        void Release(object obj);

        /// <summary>
        /// 判断类型是否已注册。
        /// </summary>
        /// <param name="type">类型参数</param>
        /// <returns>指示类型是否已注册。</returns>
        bool IsRegistered(Type type);
    }
}
