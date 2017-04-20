using Example.Dto.UI.easyUI;
using Ls;
using Ls.Dto;
using Ls.Dto.Response;
using Ls.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example.Mvc.Framework.Base
{
    public abstract class BaseController : Ls.Mvc.LsControllerBase
    {
        public BaseController()
        {
            //ActionInvoker = IocManager.Instance.Resolve<IActionInvoker>();
        }

        #region 属性
        /// <summary>
        /// Json数据
        /// </summary>
        protected virtual string JsonData { get; set; }
        #endregion

        #region 返回
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <typeparam name="TResult">返回内容泛型</typeparam>
        /// <param name="result">结果内容</param>
        /// <returns></returns>
        protected virtual ContentResult ResultSuccess<TResult>(TResult result)
        {
            ResponseDto<TResult> response = new ResponseDto<TResult> { ResponseCode = ResponseCode.Success, Description = "success!", Content = result };
            return Content(JsonString(response));
        }

        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="description">失败描述</param>
        /// <returns></returns>
        protected virtual ContentResult ResultFailure(string description)
        {
            ResponseDto<BaseDto> response = new ResponseDto<BaseDto> { ResponseCode = ResponseCode.Failure, Description = description, Content = null };
            return Content(JsonString(response));
        }
        /// <summary>
        /// 转化为json格式字符串
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        protected string JsonString(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        protected virtual ContentResult ResultDataGrid<T>(IList<T> data, Pager pager, object footer = null)
            where T : BaseDto
        {
            DataGrid<T> gridData = new DataGrid<T>
            {
                rows = data.ToList(),
                total = pager.totalCount,
                footer = footer
            };
            return Content(JsonString(gridData));
        }

        protected virtual ContentResult ResultTreeGrid<T>(IList<T> data)
            where T : BaseDto
        {
            DataGrid<T> gridData = new DataGrid<T>
            {
                rows = data.ToList()
            };
            return Content(JsonString(gridData));
        }

        #endregion

        #region 重写方法

        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
        {
            return base.BeginExecute(requestContext, callback, state);
        }
        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            var platEx = filterContext.Exception as Ls.LsException;
            ResponseDto<BaseDto> response = new ResponseDto<BaseDto> { ResponseCode = ResponseCode.Failure, Description = filterContext.Exception.Message, Content = null };
            if (platEx != null)
            {
                if (platEx.LsExceptionEnum == LsExceptionEnum.NoLogin)
                {
                    response.ResponseCode = ResponseCode.Expired;
                    response.Description = "登录过期~";
                }
                else if (platEx.LsExceptionEnum == LsExceptionEnum.NoPermission)
                {
                    response.ResponseCode = ResponseCode.Failure;
                    response.Description = "您没有访问此方法或页面的权限~";
                }

            }
            else
            {
                Log.Error("系统错误", filterContext.Exception);
            }
            filterContext.ExceptionHandled = true;
            filterContext.RequestContext.HttpContext.Response.Write(JsonString(response));
            filterContext.RequestContext.HttpContext.Response.StatusCode = 200;
            filterContext.RequestContext.HttpContext.Response.End();

        }
        #endregion
    }
}