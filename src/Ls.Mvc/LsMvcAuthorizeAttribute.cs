using System.Linq;
using Ls.Authorization;
using Ls.Domain.UnitOfWork;
using Ls.IoC;
using Ls.Utilities;

namespace Ls.Mvc
{
    /// <summary>
    /// 授权
    /// </summary>
    public class LsMvcAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        public string[] Permissions { get; set; }


        public LsMvcAuthorizeAttribute(params string[] permissions)
        {
            Permissions = permissions;
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            if (!base.AuthorizeCore(httpContext))
            {
                return false;
            }
            if (!Permissions.Any()) return true;
            try
            {
                long perId = SafeConvert.ToInt64(httpContext.Request.Params["perId"]);
                var requestUrl = httpContext.Request.Url.LocalPath;
                using (var uow=IocManager.Instance.Resolve<IUnitOfWorkProvider>().NewUnitOfWork())
                {
                    uow.Begin();
                    IocManager.Instance.Resolve<IPermissionChecker>().Check(perId, requestUrl);
                    uow.Complete();
                }
                return true;
            }
            catch (System.Exception exception)
            {
                return false;
            }
        }
    }
}