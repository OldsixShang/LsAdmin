using System.Web.Mvc;
using Example.Application.ServiceInterfaces.Sys;
using Example.Mvc.Framework.Base;
using Example.Dto.Sys.ActionManage;
using Ls.Model;
using Ls.Utilities;

namespace Example.Mvc.Areas.SystemManage.Controllers
{
    public class ActionManageController : BaseController
    {
        private IActionService _actionService;
        public ActionManageController(IActionService actionService)
        {
            _actionService = actionService;
        }

        public ActionResult Index()
        {
            return View(new PageDto());
        }
        public ContentResult Query(QueryConditionDto dto,Pager pager)
        {
            var dtos = _actionService.QueryPagerAction(dto, pager);
            return  ResultDataGrid<ActionDto>(dtos,pager);
        }

        public ActionResult ActionEdit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return PartialView("_editForm",new ActionDto());
            else
            {
                ActionDto btnDto = _actionService.GetAction( id);
                return PartialView("_editForm", btnDto);
            }
        }
        public ContentResult Add(ActionDto dto)
        {
            _actionService.AddAction(dto);
            return ResultSuccess<string>("添加成功");
        }

        public ContentResult Modify(ActionDto dto)
        {
            _actionService.ModifyAction(dto);
            return ResultSuccess<string>("修改成功");
        }

        public ContentResult Remove(string id)
        {
            _actionService.DeleteAction(new ActionDto { Id = id});
            return ResultSuccess<string>("删除成功");
        }
	}
}