using System.Collections.Generic;
using System.Reflection;

namespace Ls.Reflection {
    /// <summary>
    /// 程序集查找器接口。
    /// </summary>
    public interface IAssemblyFinder {
        /// <summary>
        /// 获取所有的程序集。
        /// </summary>
        /// <returns>返回程序集列表。</returns>
        List<Assembly> GetAllAssemblies();
    }
}
