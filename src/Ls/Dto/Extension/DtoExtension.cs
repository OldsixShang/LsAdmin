using Ls.Dto;
using System.Collections.Generic;
using System.Linq;

namespace Ls.Dto.Extension
{
    /// <summary>
    /// EasyUI数据传输实体拓展
    /// </summary>
    public static class DtoExtension
    {
        #region 转化为标准格式的树列表
        /// <summary>
        /// 转化为标准格式的树列表
        /// </summary>
        /// <param name="source">一般格式的树列表</param>
        /// <returns>标准格式的树列表</returns>
        public static List<T> ToStandardFormatTree<T>(this List<T> source)
            where T : TreeNodeDto<T>
        {
            List<T> roots = source.Where(t => !t.ParentId.HasValue).ToList();
            BuidStandardTree(roots, source);
            return roots;
        }
        /// <summary>
        /// 构建标准树
        /// </summary>
        /// <param name="nodes">节点列表</param>
        /// <param name="source">节点资源</param>
        private static void BuidStandardTree<T>(List<T> nodes, List<T> source)
            where T : TreeNodeDto<T>
        {
            foreach (var node in nodes)
            {
                var children = source.Where(t => t.ParentId == node.Id).ToList();
                if (children.Count > 0)
                {
                    node.Children = children;
                    BuidStandardTree(node.Children, source);
                }
                else
                    node.Children = null;
            }
        }
        #endregion
    }
}
