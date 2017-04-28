using Example.Application.ServiceInterfaces.Sys;
using Ls.IoC;
using Ls.Mvc.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Example.Mvc.Framework.Auth
{
    public class PermissionValidator : IPermissionValidator
    {
        private readonly IPermissionService _permissionService;
        public PermissionValidator()
        {
            _permissionService = IocManager.Instance.Resolve<IPermissionService>();
        }
        public bool Validate(string requestUrl, string menuId, string actionTemplate)
        {
            var permissions = _permissionService.QueryActionPermission(menuId);
            var permissionList = permissions.Where(t => t.Template == actionTemplate).Where(t => t.Url.Contains(requestUrl));
            return permissionList.Count() > 0;
        }
    }
}