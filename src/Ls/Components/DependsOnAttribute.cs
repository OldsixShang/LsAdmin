using System;

namespace Ls.Components {
    /// <summary>
    /// 组件依赖特性。
    /// 设置组件之间的依赖关系。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DependsOnAttribute : Attribute {
        /// <summary>
        /// 组件依赖的类型。
        /// </summary>
        public Type[] DependedComponentTypes { get; private set; }

        /// <summary>
        /// 创建<see cref="DependsOnAttribute"/>类型的对象。
        /// </summary>
        /// <param name="dependedComponentTypes">组件依赖的类型</param>
        public DependsOnAttribute(params Type[] dependedComponentTypes) {
            DependedComponentTypes = dependedComponentTypes;
        }
    }
}
