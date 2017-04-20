using System.Collections.Generic;
using Ls.Model;
using Example.Dto.Sys.PermissionManage;
using Example.Dto;
using Example.Dto.Auth;

namespace Tts.Platform.Application.ServiceInterfaces.Sys
{
    public interface IPermissionService
    {
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="Id">权限唯一标识</param>
        /// <returns>权限信息</returns>
        PermissionDto GetPermission(long Id);
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="dto">权限信息</param>
        void AddPermission(PermissionDto dto);
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="dto">权限信息</param>
        void DeletePermission(PermissionDto dto);
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="dto">权限信息</param>
        void ModifyPermission(PermissionDto dto);

        /// <summary>
        /// 查询权限信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>权限信息</returns>
        IList<PermissionDto> QueryPermission(QueryConditionDto conditionDto);

        /// <summary>
        /// 查询所有权限信息
        /// </summary>
        /// <returns>权限信息</returns>
        IList<PermissionMenuActionDto> QueryAllPermission(long? roleId = -1);
        /// <summary>
        /// 分页查询权限信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>权限信息</returns>
        IList<PermissionDto> QueryPagerPermission(QueryConditionDto conditionDto, Pager pager);
        
        #region 查询菜单权限
        /// <summary>
        /// 查询左侧菜单栏权限信息
        /// </summary>
        /// <returns>左侧菜单栏权限列表</returns>
        IList<PermissionMenuDto> QueryNavMenuPermission();

        /// <summary>
        /// 查询固定菜单权限信息
        /// </summary>
        /// <returns>固定菜单权限列表</returns>
        IList<PermissionMenuDto> QuerySolidMenuPermission();

        /// <summary>
        /// 查询头部菜单权限信息
        /// </summary>
        /// <returns>头部菜单权限列表</returns>
        IList<PermissionMenuDto> QueryTopNavMenuPermission(); 
        #endregion

        /// <summary>
        /// 查询操作权限信息
        /// </summary>
        /// <param name="menuPermissionId">菜单权限Id</param>
        /// <returns>操作权限信息</returns>
        IList<PermissionDto> QueryActionPermission(long menuPermissionId);
    }
}
