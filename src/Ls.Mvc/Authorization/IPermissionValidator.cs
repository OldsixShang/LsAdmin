using Ls.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Mvc.Authorization
{
    /// <summary>
    /// 权限验证器
    /// </summary>
    public interface IPermissionValidator: ITransientDependency
    {
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="requestUrl">请求地址</param>
        /// <param name="menuId">权限Id</param>
        /// <param name="actionTemplate">操作名称</param>
        /// <returns></returns>
        bool Validate(string requestUrl, string menuId, string actionTemplate);
    }
}
