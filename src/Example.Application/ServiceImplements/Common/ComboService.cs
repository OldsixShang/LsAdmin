using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using Example.Dto.UI.easyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using Ls.Dto.Extension;
using Example.Application.ServiceInterfaces.Common;

namespace Example.Application.ServiceImplements.Common
{
    /// <summary>
    /// 角色领域服务
    /// </summary>
    public class ComboService : BaseService, IComboService
    {
        #region 字段
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IActionRepository _actionRepository;
        private readonly IMenuRepository _menuRepository;
        #endregion

        public ComboService(
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository,
            IActionRepository actionRepository,
            IMenuRepository menuRepository)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _actionRepository = actionRepository;
            _menuRepository = menuRepository;
        }


        #region 系统管理
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="remote">异步请求</param>
        /// <param name="remoteCount">异步请求数量</param>
        /// <param name="name">角色名称</param>
        /// <returns>角色下拉列表</returns>
        public List<ComboboxItem> GetRoles(bool remote = false, int remoteCount = 20, string name = "")
        {
            var query = _roleRepository.GetTable();
            if (remote) query = query.Take(remoteCount);
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            return query.Select(t => new ComboboxItem
            {
                Id = t.Id.ToString(),
                Text = t.Name
            }).ToList();
        }
        /// <summary>
        /// 获取操作列表
        /// </summary>
        /// <param name="remote">是否为异步请求</param>
        /// <param name="remoteCount">每次异步请求数量</param>
        /// <param name="name">数量</param>
        /// <returns></returns>
        public List<ComboboxItem> GetActions(bool remote = false, int remoteCount = 20, string name = "")
        {
            var query = _actionRepository.GetTable();
            if (remote) query = query.Take(remoteCount);
            if (!string.IsNullOrEmpty(name)) query = query.Where(t => t.Name.Contains(name));
            return query.Select(t => new ComboboxItem
            {
                Id = t.Id.ToString(),
                Text = t.Name
            }).ToList();
        }
        /// <summary>
        /// 获取所有菜单类型列表
        /// </summary>
        /// <returns>菜单类型列表</returns>
        public List<ComboboxItem> GetMenuTypes()
        {
            List<ComboboxItem> results = new List<ComboboxItem>();
            foreach (MenuType value in Enum.GetValues(typeof(MenuType)))
            {
                results.Add(new ComboboxItem { Id = value.GetHashCode().ToString(), Text = value.ToString() });
            }
            return results;
        }
        /// <summary>
        /// 获取所有菜单树列表
        /// </summary>
        /// <returns>菜单树列表</returns>
        public List<ComboboxItem> GetMenus()
        {
            var query = _menuRepository.GetTable();
            return query.Select(t => new ComboboxItem
            {
                Id = t.Id.ToString(),
                Text = t.Name
            }).ToList();
        }
        /// <summary>
        /// 获取所有菜单树列表
        /// </summary>
        /// <returns>菜单树列表</returns>
        public List<Dto.UI.easyUI.ComboTreeItem> GetAllMenus()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        /// <returns>权限列表</returns>
        public List<ComboTreeItem> GetAllPermissions()
        {
            var items = _permissionRepository.GetTable()
                .Select(t => new ComboTreeItem
                {
                    Id = t.Id,
                    Text = t.Name,
                    ParentId = t.ParentId
                }).ToList();
            return items.ToStandardFormatTree();
        }

        #endregion
    }
}
