using Ls.Domain.Repositories;
using Example.Domain.Entities.Authorization;

namespace Example.Domain.Repositories.Authorization
{
    /// <summary>
    /// 权限仓储接口
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission>
    {
        /// <summary>
        /// 查询权限列表
        /// </summary>
        /// <param name="name">权限名称</param>
        /// <returns></returns>
        dynamic QueryPermission(string name = "");
        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="id">权限id</param>
        /// <returns>权限信息</returns>
        dynamic GetPermission(long id);
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menuType">菜单类型</param>
        /// <returns>菜单权限列表</returns>
        dynamic QueryMenuPermission(long roleId, MenuType menuType);
        /// <summary>
        /// 获取操作权限
        /// </summary>
        /// <param name="roleId">角色id</param>
        /// <param name="menuId">菜单id</param>
        /// <returns>操作权限列表</returns>
        dynamic QueryActionPermission(long roleId, long menuId);
    }
}
