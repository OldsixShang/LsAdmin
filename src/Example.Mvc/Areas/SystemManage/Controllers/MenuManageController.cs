using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Example.Application.ServiceInterfaces.Sys;
using Example.Mvc.Framework.Base;
using Example.Dto.Sys.MenuManage;
using Ls.Model;
using Example.Application.ServiceInterfaces.Common;
using Ls.Utilities;

namespace Example.Areas.SystemManage.Controllers
{
    public class MenuManageController : BaseController
    {
        private IMenuService _menuService;
        private IComboService _comboService;
        public MenuManageController(
            IMenuService menuService,
            IComboService comboService)
        {
            _menuService = menuService;
            _comboService = comboService;
        }

        protected override string JsonData
        {
            get
            {
                return JsonString(new Dictionary<string, object> 
                { 
                    {"MenuTypes",_comboService.GetMenuTypes()}
                });
            }
        }

        public ActionResult Index()
        {
            ViewBag.JsonData = JsonData;
            return View(new PageDto());
        }
        public ContentResult Query(QueryConditionDto dto, Pager pager)
        {
            var dtos = _menuService.QueryPagerMenu(dto, pager);
            return ResultDataGrid<MenuDto>(dtos, pager);
        }

        public ActionResult MenuEdit(string id)
        {
            ViewBag.JsonData = JsonData;
            if (string.IsNullOrEmpty(id))
                return PartialView("_editForm", new MenuDto());
            else
            {
                MenuDto btnDto = _menuService.GetMenu(SafeConvert.ToInt64(id));
                return PartialView("_editForm", btnDto);
            }
        }
        public ContentResult Add(MenuDto dto)
        {
            _menuService.AddMenu(dto);
            return ResultSuccess<string>("添加成功");
        }

        public ContentResult Modify(MenuDto dto)
        {
            _menuService.ModifyMenu(dto);
            return ResultSuccess<string>("修改成功");
        }

        public ContentResult Remove(string id)
        {
            _menuService.DeleteMenu(new MenuDto { Id = SafeConvert.ToInt64(id) });
            return ResultSuccess<string>("删除成功");
        }
    }
}