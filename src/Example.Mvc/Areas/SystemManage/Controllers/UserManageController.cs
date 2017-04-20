using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Example.Application.ServiceInterfaces;
using Example.Application.ServiceInterfaces.Common;
using Example.Application.ServiceInterfaces.Sys;
using Ls.Mvc.Authorization;
using Example.Mvc.Framework.Base;
using Example.Dto.Sys.UserManage;
using Ls.Utilities;
using Ls.Model;

namespace Example.Areas.SystemManage.Controllers
{
    public class UserManageController : BaseController
    {
        private IUserService _userService;
        private IRoleService _roleService;
        private IComboService _comboService;
        public UserManageController(
            IUserService userService,
            IRoleService roleService,
            IComboService comboService
            )
        {
            _userService = userService;
            _roleService = roleService;
            _comboService = comboService;
        }

        protected override string JsonData
        {
            get
            {
                return JsonString(new Dictionary<string, object> 
                { 
                    {"Roles",_comboService.GetRoles()}
                });
            }
        }
        [ActionTemplate(Template = ActionTemplate.Query)]
        public ActionResult Index()
        {
            //页面初始化数据
            ViewBag.JsonData = JsonData;
            return View(new PageDto());
        }

        [ActionTemplate(Template = ActionTemplate.Query)]
        public ContentResult Query(QueryConditionDto dto, Pager pager)
        {
            var dtos = _userService.QueryPagerUser(dto, pager);
            return ResultDataGrid<UserDto>(dtos, pager);
        }

        public ActionResult UserEdit(string id)
        {
            //页面初始化数据
            ViewBag.JsonData = JsonData;
            if (string.IsNullOrEmpty(id))
                return PartialView("_editForm", new UserDto());
            else
            {
                UserDto btnDto = _userService.GetUser(SafeConvert.ToInt64(id));
                return PartialView("_editForm", btnDto);
            }
        }

        [ActionTemplate(Template = ActionTemplate.Add)]
        public ContentResult Add(UserDto dto)
        {
            _userService.AddUser(dto);
            return ResultSuccess<string>("添加成功");
        }

        [ActionTemplate(Template = ActionTemplate.Modify)]
        public ContentResult Modify(UserDto dto)
        {
            _userService.ModifyUser(dto);
            return ResultSuccess<string>("修改成功");
        }

        [ActionTemplate(Template = ActionTemplate.Delete)]
        public ContentResult Remove(long? id)
        {
            _userService.DeleteUser(new UserDto { Id = id});
            return ResultSuccess<string>("删除成功");
        }
    }
}