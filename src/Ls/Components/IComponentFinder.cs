using System;
using System.Collections.Generic;

namespace Ls.Components {
    /// <summary>
    /// 组件查找器接口。
    /// </summary>
    public interface IComponentFinder {
        /// <summary>
        /// 查找所有组件。
        /// </summary>
        /// <returns>返回组件列表。</returns>
        List<Type> FindAll();
    }
}
