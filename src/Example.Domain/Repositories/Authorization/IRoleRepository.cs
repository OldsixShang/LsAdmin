using Ls.Domain.Repositories;
using Ls.Model;
using System.Collections.Generic;
using Example.Domain.Entities.Authorization;

namespace Example.Domain.Repositories.Authorization
{
    /// <summary>
    /// 角色仓储接口
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// 分页查询角色信息
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="pager">分页信息</param>
        /// <returns>角色信息</returns>
        List<Role> QueryPager(string name,Pager pager);
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <returns>角色信息列表</returns>
        List<Role> Query(string name);
        /// <summary>
        /// 异步查询角色信息列表
        /// </summary>
        /// <param name="name">角色名称</param>
        /// <param name="remoteCount">异步查询数量</param>
        /// <returns>角色信息</returns>
        List<Role> QueryRemote(string name,int remoteCount = 20);
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="id">角色Id</param>
        /// <returns></returns>
        Role GetRole(long id);
    }
}
