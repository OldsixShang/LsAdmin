using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using System;
using System.Collections.Generic;
using Example.Application.ServiceInterfaces.Sys;
using Ls.Utilities;
using Ls.Model;
using Ls.Caching;
using Example.Dto.Sys.PermissionManage;
using Ls.Authorization;
using System.Linq;
using Example.Dto;
using Ls.Dto.Extension;
using Example.Dto.Auth;
using AutoMapper.QueryableExtensions;
using EmitMapper;
using EmitMapper.MappingConfiguration;

namespace Example.Application.ServiceImplements.Sys
{
    /// <summary>
    /// 权限领域服务
    /// </summary>
    public class PermissionService : BaseService, IPermissionService
    {
        #region 字段
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAuthStore _authStore;
        private readonly ICacheManager _cacheManager;
        #endregion

        public PermissionService(
            IPermissionRepository permissionRepository,
            IRoleRepository roleRepository,
            IAuthStore authStore,
            ICacheManager cacheManager
            )
        {
            _permissionRepository = permissionRepository;
            _roleRepository = roleRepository;
            _authStore = authStore;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取权限信息
        /// </summary>
        /// <param name="Id">权限唯一标识</param>
        /// <returns>权限信息</returns>
        public PermissionDto GetPermission(string Id)
        {
            var entity = _permissionRepository.GetPermission(Id);
            return entity.ToDto<PermissionDto>();
        }
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="dto">传入权限信息</param>
        public void AddPermission(PermissionDto dto)
        {
            Permission entity = dto.ToEntity<Permission>();
            Permission parent = _permissionRepository.Get(dto.ParentId);
            entity.Parent = parent;
            _permissionRepository.Add(entity);
        }
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="dto">传入权限信息</param>
        public void DeletePermission(PermissionDto dto)
        {
            Permission entity = _permissionRepository.Get(t => t.Id == dto.Id);
            Permission permission = _permissionRepository.Get(dto.Id);
            _permissionRepository.Delete(permission);
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="dto">传入权限信息</param>
        public void ModifyPermission(PermissionDto dto)
        {
            Permission parentPermission = _permissionRepository.Get(dto.ParentId);
            Permission entity = _permissionRepository.Get(dto.Id);
            entity.MenuId = dto.MenuId;
            entity.ActionId = dto.ActionId;
            entity.Name = dto.Name;
            entity.Parent = parentPermission;
            _permissionRepository.Update(entity);
        }
        /// <summary>
        /// 查询权限信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>权限信息</returns>
        public IList<PermissionDto> QueryPermission(QueryConditionDto conditionDto)
        {
            dynamic entities = _permissionRepository.QueryPermission(conditionDto.PermissionName);
            return AutoMapExtensions.ToDtoList<Permission, PermissionDto>(entities);
        }
        /// <summary>
        /// 分页查询权限信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>分页权限信息</returns>
        public IList<PermissionDto> QueryPagerPermission(QueryConditionDto conditionDto, Pager pager)
        {
            throw new NotImplementedException();
        }

        #region 菜单权限
        public IList<PermissionMenuDto> QueryNavMenuPermission()
        {
            var entities = _permissionRepository.QueryMenuPermission(LsSession.RoleId, MenuType.左侧导航栏);
            List<PermissionMenuDto> dtos = AutoMapExtensions.ToDtoList<Permission,PermissionMenuDto>(entities);
            return dtos.ToStandardFormatTree();
        }
        public IList<PermissionMenuDto> QuerySolidMenuPermission()
        {
          
            var user = _authStore.GetUser(LsSession.UserId);
            var entities = _permissionRepository.QueryMenuPermission(LsSession.RoleId, MenuType.固定);
            List<PermissionMenuDto> dtos = AutoMapExtensions.ToDtoList<Permission, PermissionMenuDto>(entities);
            return dtos.ToStandardFormatTree();
        }
        public IList<PermissionMenuDto> QueryTopNavMenuPermission()
        {
            var user = _authStore.GetUser(LsSession.UserId);
            var entities = _permissionRepository.QueryMenuPermission(LsSession.RoleId, MenuType.顶部快捷菜单栏);
            List<PermissionMenuDto> dtos = AutoMapExtensions.ToDtoList<Permission, PermissionMenuDto>(entities);
            return dtos.ToStandardFormatTree();
        }
        #endregion

        public IList<PermissionDto> QueryActionPermission(string menuPermissionId)
        {
            var role = _authStore.GetRole(LsSession.RoleId);
            string rolePermissionCacheKey = string.Format(CacheKeys.ALL_PERMISSIONS, role.Id);
            if (!_cacheManager.IsSet(rolePermissionCacheKey))
            {
                var entities = _permissionRepository.QueryActionPermission(role.Id,null);
                var allPermission = entities.ToDtoList<Permission,PermissionDto>();
                //缓存权限数据
                _cacheManager.Set(rolePermissionCacheKey, allPermission, 20);
            }
            List<PermissionDto> allRolePermission = _cacheManager.Get<List<PermissionDto>>(rolePermissionCacheKey);
            return allRolePermission.Where(t=>t.ParentId == menuPermissionId).ToList();
        }


        public IList<PermissionMenuActionDto> QueryAllPermission(string roleId)
        {
            Role role = _roleRepository.GetRole((string.IsNullOrEmpty(roleId)? LsSession.RoleId : roleId));
            var perms = role.Permissions.Select(t => t.Id).ToArray();
            dynamic entities = _permissionRepository.QueryPermission(string.Empty);
            List<PermissionMenuActionDto> dtos = AutoMapExtensions.ToDtoList<Permission, PermissionMenuActionDto>(entities);
            dtos.ForEach(t => t.Checked = perms.Contains(t.Id) ? (bool?)(dtos.Where(d => d.ParentId == t.Id).Count() == 0) : null);
            return dtos.ToStandardFormatTree();

        }
    }
}
