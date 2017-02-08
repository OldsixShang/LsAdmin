using Ls.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ls.Components {
    /// <summary>
    /// 组件查找器。
    /// </summary>
    public class ComponentFinder : IComponentFinder {
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 根据类型查找器构建组件查找器。
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        public ComponentFinder(ITypeFinder typeFinder) {
            _typeFinder = typeFinder;
        }

        /// <summary>
        /// 查找所有组件。
        /// </summary>
        /// <returns>返回组件列表。</returns>
        public List<Type> FindAll() {
            return _typeFinder.GetAllTypes(ComponentBase.IsComponent).ToList();
        }
    }
}
