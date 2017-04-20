using System.Collections.Generic;
using Ls.Model;
using Example.Dto.Sys.RoleManage;
using Example.Dto;

namespace Example.Application.ServiceInterfaces.Sys
{
    public interface IRoleService
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="Id">角色唯一标识</param>
        /// <returns>角色信息</returns>
        RoleDto GetRole(long Id);
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">角色信息</param>
        void AddRole(RoleDto dto);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="dto">角色信息</param>
        void DeleteRole(RoleDto dto);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto">角色信息</param>
        void ModifyRole(RoleDto dto);

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>角色信息</returns>
        IList<RoleDto> QueryRole(QueryConditionDto conditionDto);
        /// <summary>
        /// 分页查询角色信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>角色信息</returns>
        IList<RoleDto> QueryPagerRole(QueryConditionDto conditionDto, Pager pager);
        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <param name="permissionIds">权限列表</param>
        void DistributePermission(long? roleId,long?[] permissionIds);

    }
}
