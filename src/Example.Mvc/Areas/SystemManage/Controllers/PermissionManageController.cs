using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Example.Application.ServiceInterfaces.Common;
using Example.Application.ServiceInterfaces.Sys;
using Ls.Mvc.Authorization;
using Example.Mvc.Framework.Base;
using Ls.Utilities;
using Ls.Model;
using Example.Dto.Sys.PermissionManage;

namespace Example.Areas.SystemManage.Controllers
{
    public class PermissionManageController : BaseController
    {
        private IPermissionService _permissionService;
        private IRoleService _roleService;
        private IComboService _comboService;
        public PermissionManageController(
            IPermissionService permissionService,
            IComboService comboService,
            IRoleService roleService)
        {
            _permissionService = permissionService;
            _roleService = roleService;
            _comboService = comboService;
        }

        protected override string JsonData
        {
            get
            {
                return JsonString(
                    new Dictionary<string, object>{
                    {"Permissions",_comboService.GetAllPermissions()},
                    {"Actions",_comboService.GetActions()},
                    {"Menus",_comboService.GetMenus()}
                });
            }
        }

        public ActionResult Index()
        {
            return View(new PageDto());
        }

        public ContentResult Query(QueryConditionDto dto, Pager pager)
        {
            var dtos = _permissionService.QueryPermission(dto);
            return ResultDataGrid<PermissionDto>(dtos, pager);
        }

        public ActionResult PermissionEdit(string id)
        {
            //页面初始化数据
            ViewBag.JsonData = JsonData;
            if (string.IsNullOrEmpty(id))
                return PartialView("_editForm", new PermissionDto());
            else
            {
                PermissionDto btnDto = _permissionService.GetPermission(Convert.ToInt64(id));
                return PartialView("_editForm", btnDto);
            }
        }
        public ContentResult Add(PermissionDto dto)
        {
            _permissionService.AddPermission(dto);
            return ResultSuccess<string>("添加成功");
        }

        public ContentResult Modify(PermissionDto dto)
        {
            _permissionService.ModifyPermission(dto);
            return ResultSuccess<string>("修改成功");
        }

        public ContentResult Remove(long? id)
        {
            _permissionService.DeletePermission(new PermissionDto { Id = id });
            return ResultSuccess<string>("删除成功");
        }
    }
}