using System;

namespace Ls.Reflection {
    /// <summary>
    /// 类型查找器接口。
    /// </summary>
    public interface ITypeFinder {
        /// <summary>
        /// 获取所有类型。
        /// </summary>
        /// <returns>返回类型数组。</returns>
        Type[] GetAllTypes();

        /// <summary>
        /// 获取符合条件的所有类型。
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回类型数组。</returns>
        Type[] GetAllTypes(Func<Type, bool> predicate);
    }
}
