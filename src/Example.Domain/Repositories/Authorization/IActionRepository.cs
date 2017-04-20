using Ls.Domain.Repositories;
using Ls.Model;
using System.Collections.Generic;
using Example.Domain.Entities.Authorization;

namespace Example.Domain.Repositories.Authorization
{
    /// <summary>
    /// 系统操作仓储接口
    /// </summary>
    public interface IActionRepository :IRepository<AuthAction>
    {
        /// <summary>
        /// 分页查询系统操作信息
        /// </summary>
        /// <param name="name">系统操作名称</param>
        /// <param name="pager">分页信息</param>
        /// <returns>系统操作信息</returns>
        List<AuthAction> QueryPager(string name, Pager pager);
        /// <summary>
        /// 查询系统操作信息
        /// </summary>
        /// <param name="name">系统操作名称</param>
        /// <returns>系统操作信息列表</returns>
        List<AuthAction> Query(string name);
    }
}
