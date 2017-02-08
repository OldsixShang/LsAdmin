using Ls.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ls.Reflection {
    /// <summary>
    /// 类型查找器。
    /// </summary>
    public class TypeFinder : ITypeFinder {
        /// <summary>
        /// 程序集查找器。
        /// </summary>
        public IAssemblyFinder AssemblyFinder { get; set; }

        /// <summary>
        /// 使用程序集查找器创建类型查找器。
        /// </summary>
        /// <param name="assemblyFinder">程序集查找器</param>
        public TypeFinder(IAssemblyFinder assemblyFinder) {
            AssemblyFinder = assemblyFinder;
        }

        /// <summary>
        /// 获取所有类型。
        /// </summary>
        /// <returns>返回类型数组。</returns>
        public Type[] GetAllTypes() {
            return FindAllTypes().ToArray();
        }

        /// <summary>
        /// 获取符合条件的所有类型。
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回类型数组。</returns>
        public Type[] GetAllTypes(Func<Type, bool> predicate) {
            return FindAllTypes().Where(predicate).ToArray();
        }

        private List<Type> FindAllTypes() {
            List<Type> result = new List<Type>();
            foreach (Assembly assembly in AssemblyFinder.GetAllAssemblies().Distinct()) {
                Type[] typesInThisAssembly;

                try {
                    typesInThisAssembly = assembly.GetTypes();
                } catch (ReflectionTypeLoadException ex) {
                    typesInThisAssembly = ex.Types;
                }

                if (typesInThisAssembly.IsNullOrEmpty()) {
                    continue;
                }

                result.AddRange(typesInThisAssembly.Where(type => type != null));
            }

            return result;
        }
    }
}
