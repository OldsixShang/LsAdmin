using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using Example.Dto.Sys.PermissionManage;
using Example.Dto.Sys.RoleManage;
using Ls;
using Ls.Caching;
using Ls.Model;
using Ls.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Tts.Platform.Application.ServiceInterfaces.Sys;

namespace Tts.Platform.Application.ServiceImplements.Sys
{
    /// <summary>
    /// 角色领域服务
    /// </summary>
    public class RoleService : BaseService, IRoleService
    {
        #region 字段
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly ICacheManager _cacheManager;
        #endregion

        public RoleService(IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            ICacheManager cacheManager)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <param name="Id">角色唯一标识</param>
        /// <returns>角色信息</returns>
        public RoleDto GetRole(long Id)
        {
            Role entity = _roleRepository.Get(Id);
            return entity.ToDto<RoleDto>();
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">传入角色信息</param>
        public void AddRole(RoleDto dto)
        {
            #region 业务验证
            Role role = _roleRepository.Get(t => t.Name == dto.Name);
            if (role != null) throw new LsException(string.Format("角色[{0}]已经存在,请确认！", dto.Name));
            #endregion

            Role entity = dto.ToEntity<Role>();
            _roleRepository.Add(entity);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="dto">传入角色信息</param>
        public void DeleteRole(RoleDto dto)
        {
            Role entity = _roleRepository.Get(SafeConvert.ToInt64(dto.Id));
            _roleRepository.Delete(entity);
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto">传入角色信息</param>
        public void ModifyRole(RoleDto dto)
        {
            #region 业务验证
            Role r = _roleRepository.Get(t => t.Name == dto.Name && t.Id != dto.Id);
            if (r != null) throw new LsException(string.Format("角色[{0}]已经存在,请确认！", dto.Name));
            #endregion
            Role entity = _roleRepository.Get(SafeConvert.ToInt64(dto.Id));
            entity.Name = dto.Name;
            _roleRepository.Update(entity);
        }
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>角色信息</returns>
        public IList<RoleDto> QueryRole(Example.Dto.Sys.RoleManage.QueryConditionDto conditionDto)
        {
            List<Role> entities = _roleRepository.Query(conditionDto.RoleName);
            return entities.ToListDto<Role, RoleDto>();
        }
        /// <summary>
        /// 分页查询角色信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>分页角色信息</returns>
        public IList<RoleDto> QueryPagerRole(Example.Dto.Sys.RoleManage.QueryConditionDto conditionDto, Pager pager)
        {
            var entities = _roleRepository.QueryPager(conditionDto.RoleName, pager);
            return entities.ToListDto<Role, RoleDto>();
        }

        /// <summary>
        /// 分配权限
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionIds"></param>
        public void DistributePermission(long? roleId, long?[] permissionIds)
        {
            #region 分配权限
            Role entity = _roleRepository.Get(SafeConvert.ToInt64(roleId));
            var permissions = _permissionRepository.Table.Where(t => permissionIds.Contains(t.Id));
            entity.Permissions.Clear();
            entity.Permissions = permissions.ToList();
            _roleRepository.Update(entity);
            #endregion

            #region 更新缓存
            string rolePermissionCacheKey = string.Format(CacheKeys.ALL_PERMISSIONS, entity.Id);
            if (_cacheManager.IsSet(rolePermissionCacheKey))
            {
                _cacheManager.Remove(rolePermissionCacheKey);
                dynamic entities = _permissionRepository.QueryActionPermission(entity.Id, -1);
                var allPermission = AutoMapExtensions.ToDtoList<PermissionDto>(entities);
                //缓存权限数据
                _cacheManager.Set(rolePermissionCacheKey, allPermission, 20);
            }
            #endregion
           
        }
    }
}
