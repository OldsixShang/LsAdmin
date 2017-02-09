using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ls.Reflection {
    /// <summary>
    /// 当前应用程序域程序集查找器。
    /// </summary>
    public class CurrentDomainAssemblyFinder : IAssemblyFinder {
        /// <summary>
        /// 获取所有的程序集。
        /// </summary>
        /// <returns>返回程序集列表。</returns>
        public List<Assembly> GetAllAssemblies() {
            return AppDomain.CurrentDomain.GetAssemblies().ToList();
        }
    }
}
