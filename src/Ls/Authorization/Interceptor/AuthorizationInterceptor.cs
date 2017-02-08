using Castle.DynamicProxy;
using System.Reflection;

namespace Ls.Authorization.Interceptor {
    /// <summary>
    /// 权限验证拦截器。
    /// </summary>
    public class AuthorizationInterceptor : IInterceptor {
        private IPermissionChecker _permissionChecker;

        /// <summary>
        /// 创建<see cref="AuthorizationInterceptor"/>对象。
        /// </summary>
        /// <param name="permissionChecker">权限验证器</param>
        public AuthorizationInterceptor(IPermissionChecker permissionChecker) {
            _permissionChecker = permissionChecker;
        }

        /// <summary>
        /// 拦截应用层方法。
        /// </summary>
        /// <param name="invocation">方法调用</param>
        public void Intercept(IInvocation invocation) {
            var authorizationAttribute = invocation.Method.GetCustomAttribute<AuthorizeAttribute>() ??
                                         invocation.MethodInvocationTarget.GetCustomAttribute<AuthorizeAttribute>();
            if (authorizationAttribute == null) {
                invocation.Proceed();
                return;
            }

            if (authorizationAttribute.Permissions.Length <= 0) {
                throw new LsException("未指定权限名称。");
            }

            _permissionChecker.Check(authorizationAttribute.Permissions[0]);
            invocation.Proceed();
        }
    }
}
