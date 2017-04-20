using Example.Dto.UI.easyUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Application.ServiceInterfaces.Common
{
    public interface IComboService
    {
        #region 系统管理
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="remote">异步请求</param>
        /// <param name="remoteCount">异步请求数量</param>
        /// <param name="name">角色名称</param>
        /// <returns>角色下拉列表</returns>
        List<ComboboxItem> GetRoles(bool remote = false, int remoteCount = 20, string name = "");
        /// <summary>
        /// 获取操作列表
        /// </summary>
        /// <param name="remote">异步请求</param>
        /// <param name="remoteCount">异步请求数量</param>
        /// <param name="name">操作名称</param>
        /// <returns>操作列表</returns>
        List<ComboboxItem> GetActions(bool remote = false, int remoteCount = 20, string name = "");
        /// <summary>
        /// 获取所有菜单类型列表
        /// </summary>
        /// <returns>菜单类型列表</returns>
        List<ComboboxItem> GetMenuTypes();
        /// <summary>
        /// 获取所有菜单树列表
        /// </summary>
        /// <returns>菜单树列表</returns>
        List<ComboboxItem> GetMenus();
        /// <summary>
        /// 获取所有菜单树列表
        /// </summary>
        /// <returns>菜单树列表</returns>
        List<ComboTreeItem> GetAllMenus();
        /// <summary>
        /// 获取所有权限树列表
        /// </summary>
        /// <returns>权限树列表</returns>
        List<ComboTreeItem> GetAllPermissions();
        #endregion
    }
}
