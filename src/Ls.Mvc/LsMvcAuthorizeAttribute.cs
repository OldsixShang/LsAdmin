using System.Linq;
using Ls.Authorization;
using Ls.Domain.UnitOfWork;
using Ls.IoC;

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
                using (var uow=IocManager.Instance.Resolve<IUnitOfWorkProvider>().NewUnitOfWork())
                {
                    uow.Begin();
                    IocManager.Instance.Resolve<IPermissionChecker>().Check(Permissions[0]);
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