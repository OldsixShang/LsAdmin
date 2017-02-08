using Ls.IoC;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ls.Components {
    /// <summary>
    /// 框架组件基类。
    /// 系统以组件为单位进行整合。
    /// </summary>
    public abstract class ComponentBase {
        /// <summary>
        /// IoC 管理器。
        /// </summary>
        protected internal IIocManager IocManager { get; internal set; }

        /// <summary>
        /// 组件初始化前操作。
        /// </summary>
        public virtual void PreInitialize() { }

        /// <summary>
        /// 组件初始化。
        /// </summary>
        public virtual void Initialize() { }

        /// <summary>
        /// 组件初始化后操作。
        /// </summary>
        public virtual void PostInitialize() { }

        /// <summary>
        /// 关闭组件。
        /// </summary>
        public virtual void Shutdown() { }

        /// <summary>
        /// 判断类型是否为组件。
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>返回结果。</returns>
        public static bool IsComponent(Type type) {
            return
                type.IsClass &&
                !type.IsAbstract &&
                typeof(ComponentBase).IsAssignableFrom(type);
        }

        /// <summary>
        /// 查找组件依赖的组件类型。
        /// </summary>
        /// <param name="componentType">组件类型</param>
        /// <returns>返回依赖列表。</returns>
        public static List<Type> FindDependedComponentTypes(Type componentType) {
            if (!IsComponent(componentType)) {
                throw new Exception(string.Format("类型 {0} 不是 Ls 组件。", componentType.AssemblyQualifiedName));
            }

            List<Type> list = new List<Type>();

            if (componentType.IsDefined(typeof(DependsOnAttribute), true)) {
                IEnumerable<DependsOnAttribute> dependsOnAttributes = componentType.GetCustomAttributes(typeof(DependsOnAttribute), true).Cast<DependsOnAttribute>();
                foreach (DependsOnAttribute dependsOnAttribute in dependsOnAttributes) {
                    foreach (Type dependedComponentType in dependsOnAttribute.DependedComponentTypes) {
                        list.Add(dependedComponentType);
                    }
                }
            }

            return list;
        }
    }
}
