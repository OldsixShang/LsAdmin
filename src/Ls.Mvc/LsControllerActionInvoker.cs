using Ls.IoC;
using Ls.Mvc.Authorization;
using Ls.Mvc.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Ls.Mvc
{
    /// <summary>
    /// Action 执行
    /// </summary>
    public class LsControllerActionInvoker : ControllerActionInvoker
    {
        private IValidation _validation;
        private IValidationHandler _validatorHandler;
        private IPermissionValidator _permissionValidator;
        public LsControllerActionInvoker(
            IValidation validation, 
            IValidationHandler validatorHandler, 
            IPermissionValidator permissionValidator)
        {
            _validation = validation;
            _validatorHandler = validatorHandler;
            _permissionValidator = permissionValidator;
        }

        protected override ActionResult InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary<string, object> parameters)
        {
            #region 1.请求方法验证
            string pageId = controllerContext.HttpContext.Request.Params[LsMvcConst.RequestMenuIdKey];
            object[] attrs = actionDescriptor.GetCustomAttributes(typeof(ActionTemplateAttribute), true);
            if (attrs.Length > 0)
            {
                var actionTemplate = (actionDescriptor.GetCustomAttributes(true)[0] as ActionTemplateAttribute).ActionTemplate;
                var pageUrl = ((System.Web.Routing.Route)(controllerContext.RouteData.Route)).Url.Split('/')[0] + "/" + controllerContext.RouteData.Values["controller"];
                if (!_permissionValidator.Validate(pageUrl, pageId, actionTemplate))
                    throw new LsException("抱歉，你没有此操作权限！", LsExceptionEnum.NoPermission);
            }
            #endregion

            #region 2.请求参数验证
            foreach (var p in parameters)
            {
                var results = _validation.Validate(p.Value);
                _validatorHandler.Handle(results);
            } 
            #endregion

            return base.InvokeActionMethod(controllerContext, actionDescriptor, parameters);
        }
    }
}
