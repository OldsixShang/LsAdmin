using System;
using System.Collections.Generic;

namespace Ls.Extensions {
    /// <summary>
    /// <see cref="ICollection{T}"/>集合扩展。
    /// </summary>
    public static class CollectionExtension {
        /// <summary>
        /// 判断集合是否为空或 null 。
        /// </summary>
        /// <typeparam name="T">集合元素类型参数</typeparam>
        /// <param name="source">集合</param>
        /// <returns>返回集合是否为空或 null 。</returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> source) {
            return source == null || source.Count <= 0;
        }

        /// <summary>
        /// 按依据对列表排序。
        /// </summary>
        /// <typeparam name="T">列表元素类型</typeparam>
        /// <param name="source">源列表</param>
        /// <param name="getDependencies">排序依据</param>
        /// <returns>返回排序后的列表。</returns>
        public static IList<T> SortByDependencies<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getDependencies) {
            var sorted = new List<T>();
            var visited = new Dictionary<T, bool>();

            foreach (var item in source) {
                SortByDependenciesVisit(item, getDependencies, sorted, visited);
            }

            return sorted;
        }

        private static void SortByDependenciesVisit<T>(T item, Func<T, IEnumerable<T>> getDependencies, List<T> sorted, Dictionary<T, bool> visited) {
            bool inProcess;
            var alreadyVisited = visited.TryGetValue(item, out inProcess);

            if (alreadyVisited) {
                if (inProcess) {
                    throw new ArgumentException("Cyclic dependency found!");
                }
            } else {
                visited[item] = true;

                var dependencies = getDependencies(item);
                if (dependencies != null) {
                    foreach (var dependency in dependencies) {
                        SortByDependenciesVisit(dependency, getDependencies, sorted, visited);
                    }
                }

                visited[item] = false;
                sorted.Add(item);
            }
        }
    }
}
