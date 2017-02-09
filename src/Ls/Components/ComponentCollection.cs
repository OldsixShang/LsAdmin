using Ls.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Ls.Components {
    /// <summary>
    /// 组件集合。
    /// </summary>
    internal class ComponentCollection : List<ComponentDescription> {
        public TComponent GetComponent<TComponent>()
            where TComponent : ComponentBase {
            ComponentDescription component = this.FirstOrDefault(c => c.Type == typeof(TComponent));
            return (TComponent)component.Instance;
        }

        /// <summary>
        /// 获取根据依赖关系排序后的组件描述列表。
        /// </summary>
        /// <returns>返回排序后的列表。</returns>
        public List<ComponentDescription> GetSortedComponentListByDependency() {
            return this.SortByDependencies<ComponentDescription>(c => c.Dependencies).ToList();
        }
    }
}
