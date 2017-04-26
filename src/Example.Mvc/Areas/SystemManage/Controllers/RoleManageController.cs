using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Example.Application.ServiceInterfaces;
using Example.Application.ServiceInterfaces.Common;
using Example.Application.ServiceInterfaces.Sys;
using Ls.Mvc.Authorization;
using Example.Mvc.Framework.Base;
using Example.Dto.Sys.RoleManage;
using Ls.Utilities;
using Ls.Model;
using Example.Dto;
using System.Linq;

namespace Example.Mvc.Areas.SystemManage.Controllers
{
    public class RoleManageController : BaseController
    {
        private IRoleService _roleService;
        private IPermissionService _permissionService;
        public RoleManageController(IRoleService roleService,
            IPermissionService permissionService)
        {
            _roleService = roleService;
            _permissionService = permissionService;
        }

        public ActionResult Index()
        {
            return View(new PageDto());
        }
        public ContentResult Query(QueryConditionDto dto, Pager pager)
        {
            var dtos = _roleService.QueryPagerRole(dto, pager);
            return ResultDataGrid<RoleDto>(dtos, pager);
        }

        public ActionResult RoleEdit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return PartialView("_editForm", new RoleDto());
            else
            {
                RoleDto btnDto = _roleService.GetRole(id);
                return PartialView("_editForm", btnDto);
            }
        }
        public ContentResult Add(RoleDto dto)
        {
            _roleService.AddRole(dto);
            return ResultSuccess<string>("添加成功");
        }

        public ContentResult Modify(RoleDto dto)
        {
            _roleService.ModifyRole(dto);
            return ResultSuccess<string>("修改成功");
        }

        public ContentResult Remove(string id)
        {
            _roleService.DeleteRole(new RoleDto { Id = id });
            return ResultSuccess<string>("删除成功");
        }

        public ActionResult Distribute(string id)
        {
            var allPermissions = _permissionService.QueryAllPermission(id);
            DistributePageDto distributePageDto = new DistributePageDto
            {
                RoleId = id,
                LeftNavMenuList = allPermissions.Where(p => p.MenuType == "左侧导航栏").ToList(),
                SolidNavMenuList = allPermissions.Where(p => p.MenuType == "固定").ToList(),
                TopNavMenuList = allPermissions.Where(p => p.MenuType == "顶部快捷菜单栏").ToList(),
                APIMenuList = allPermissions.Where(p => p.MenuType == "WebApi").ToList()
            };
            return PartialView("_distributePermission", distributePageDto);
        }
        /// <summary>
        /// 权限分配
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="permissionIds"></param>
        /// <returns></returns>
        public ActionResult DistributePermission(string roleId, string[] permissionIds)
        {
            _roleService.DistributePermission(roleId, permissionIds);
            return ResultSuccess<string>("分配成功！");
        }
    }
}