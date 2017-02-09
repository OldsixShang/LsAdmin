using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ls.Components {
    /// <summary>
    /// 组件描述。
    /// </summary>
    public class ComponentDescription {
        /// <summary>
        /// 组件类型。
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 组件所属程序集。
        /// </summary>
        public Assembly Assembly { get; set; }

        /// <summary>
        /// 组件实例。
        /// </summary>
        public ComponentBase Instance { get; set; }

        /// <summary>
        /// 依赖的组件。
        /// </summary>
        public List<ComponentDescription> Dependencies { get; private set; }

        /// <summary>
        /// 组件描述构造函数。
        /// </summary>
        /// <param name="instance">组件实例</param>
        public ComponentDescription(ComponentBase instance) {
            Type = instance.GetType();
            Assembly = Type.Assembly;
            Instance = instance;
            Dependencies = new List<ComponentDescription>();
        }
    }
}
