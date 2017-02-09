using Ls.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ls.Components {
    /// <summary>
    /// 组件管理器。
    /// </summary>
    public class ComponentManager : IComponentManager {
        private readonly IIocManager _iocManager;
        private readonly ComponentCollection _components;
        private readonly IComponentFinder _componentFinder;

        /// <summary>
        /// 组件管理器构造函数。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        /// <param name="componentFinder">组件查找器</param>
        public ComponentManager(IIocManager iocManager, IComponentFinder componentFinder) {
            _iocManager = iocManager;
            _componentFinder = componentFinder;
            _components = new ComponentCollection();
        }

        /// <summary>
        /// 初始化所有组件。
        /// </summary>
        public void InitializeComponents() {
            LoadAllComponents();

            List<ComponentDescription> sortedComponents = _components.GetSortedComponentListByDependency();

            sortedComponents.ForEach(c => c.Instance.PreInitialize());
            sortedComponents.ForEach(c => c.Instance.Initialize());
            sortedComponents.ForEach(c => c.Instance.PostInitialize());
        }

        /// <summary>
        /// 关闭所有组件。
        /// </summary>
        public void ShutdownComponents() {
            List<ComponentDescription> sortedComponents = _components.GetSortedComponentListByDependency();
            sortedComponents.Reverse();
            sortedComponents.ForEach(c => c.Instance.Shutdown());
        }

        /// <summary>
        /// 加载所有组件。
        /// </summary>
        private void LoadAllComponents() {
            ICollection<Type> componentTypes = AddMissingDependedComponents(_componentFinder.FindAll());

            foreach (Type componentType in componentTypes) {
                if (!ComponentBase.IsComponent(componentType)) {
                    throw new Exception(string.Format("{0} 不是 Ls 组件。", componentType.AssemblyQualifiedName));
                }

                if (!_iocManager.IsRegistered(componentType)) {
                    _iocManager.Register(componentType);
                }
            }

            foreach (Type componentType in componentTypes) {
                ComponentBase componentObject = (ComponentBase)_iocManager.Resolve(componentType);
                componentObject.IocManager = _iocManager;
                _components.Add(new ComponentDescription(componentObject));
            }

            int coreComponentIndex = _components.FindIndex(m => m.Type == typeof(LsCoreComponent));
            if (coreComponentIndex > 0) {
                ComponentDescription coreComponent = _components[coreComponentIndex];
                _components.RemoveAt(coreComponentIndex);
                _components.Insert(0, coreComponent);
            }

            SetDependencies();
        }

        private void SetDependencies() {
            foreach (ComponentDescription componentDescription in _components) {
                foreach (AssemblyName referencedAssemblyName in componentDescription.Assembly.GetReferencedAssemblies()) {
                    Assembly referencedAssembly = Assembly.Load(referencedAssemblyName);
                    List<ComponentDescription> dependedComponentList = _components.Where(m => m.Assembly == referencedAssembly).ToList();
                    if (dependedComponentList.Count > 0) {
                        componentDescription.Dependencies.AddRange(dependedComponentList);
                    }
                }

                foreach (Type dependedComponentType in ComponentBase.FindDependedComponentTypes(componentDescription.Type)) {
                    ComponentDescription dependedComponent = _components.FirstOrDefault(m => m.Type == dependedComponentType);
                    if (dependedComponent == null) {
                        throw new Exception(string.Format("未找到组件 {1} 依赖的组件 {0}。", dependedComponentType.AssemblyQualifiedName, componentDescription.Type.AssemblyQualifiedName));
                    }

                    if ((componentDescription.Dependencies.FirstOrDefault(dm => dm.Type == dependedComponentType) == null)) {
                        componentDescription.Dependencies.Add(dependedComponent);
                    }
                }
            }
        }

        private static ICollection<Type> AddMissingDependedComponents(ICollection<Type> allComponents) {
            foreach (Type component in allComponents) {
                FillDependedComponents(component, allComponents);
            }
            return allComponents;
        }

        private static void FillDependedComponents(Type component, ICollection<Type> allComponents) {
            foreach (Type dependedComponent in ComponentBase.FindDependedComponentTypes(component)) {
                if (!allComponents.Contains(dependedComponent)) {
                    allComponents.Add(dependedComponent);
                    FillDependedComponents(dependedComponent, allComponents);
                }
            }
        }
    }
}
