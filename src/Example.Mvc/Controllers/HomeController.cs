using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Example.Application.ServiceInterfaces.Sys;
using Example.Mvc.Framework.Base;
using Example.Dto;
using Example.Dto.Sys.UserManage;

namespace  Example.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        #region 字段
        IPermissionService _permissionService;
        IUserService _userService; 
        #endregion

        public HomeController(IPermissionService permissionService, IUserService userService)
        {
            _permissionService = permissionService;
            _userService = userService;
        }

        public ActionResult Index()
        {
            IndexPageDto indexPageDto = new IndexPageDto { 
                LeftNavMenuList= _permissionService.QueryNavMenuPermission().ToList(),
                SolidNavMenuList = _permissionService.QuerySolidMenuPermission().ToList(),
                TopNavMenuList = _permissionService.QueryTopNavMenuPermission().ToList()
            };
            return View(indexPageDto);
        }
        /// <summary>
        /// tab主页
        /// </summary>
        /// <returns></returns>
        public ActionResult TabIndex()
        {
            return View();
        }

        #region 登录
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult UserLogin(LoginDto dto)
        {
            return ResultSuccess(_userService.Login(dto));
        } 
        #endregion

        #region 用户信息
        public ActionResult UserInfo()
        {
            var user =  _userService.GetUser(LsSession.UserId);
            return View(user);
        }
        public ActionResult ModifyUser(UserDto dto)
        {
            _userService.ModifyUser(dto);
            return ResultSuccess<string>("修改成功");
        }
        #endregion

        #region 修改密码
        public ActionResult ModifyPassword()
        {
            return View();
        }
        public ActionResult ConfirmModifyPassword(ModifyPasswordDto dto)
        {
            _userService.ModifyPasswordDto(dto);
            return ResultSuccess<string>("修改成功！");
        }
        #endregion

        #region 注销
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            LsSession.SignOut();
            return RedirectToAction("Login");
        } 
        #endregion

        #region 错误页面
        public ActionResult ErrorPage_404()
        {
            return View();
        }
        public ActionResult ErrorPage_500()
        {
            return View();
        }
        #endregion

    }
}