using Example.Application.ServiceInterfaces.Sys;
using Example.Dto.Sys.PermissionManage;
using Ls.IoC;
using Ls.Session;
using Ls.Utilities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Example.Mvc.Framework.Base
{
    public abstract class LsWebViewPage<T> : WebViewPage<T>, ITransientDependency
    {
        IPermissionService _permissionService;
        public LsWebViewPage()
        {
            _permissionService = IocManager.Instance.Resolve<IPermissionService>();
            LsSession = IocManager.Instance.Resolve<ILsSession>();
        }
        public LsWebViewPage(IPermissionService permissionService)
        {
            this.LsSession = NullLsSession.Instance;
            _permissionService = permissionService;
        }
        public ILsSession LsSession { get; set; }

        public List<PermissionDto> PermissionData
        {
            get
            {
                string pageId = Context.Request.QueryString["pageId"];
                return _permissionService.QueryActionPermission(pageId) as List<PermissionDto>;
            }
        }
    }
}
