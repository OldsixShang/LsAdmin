using Castle.DynamicProxy;

namespace Ls.Validation.Interception {
    /// <summary>
    /// 参数验证拦截器。
    /// </summary>
    public class ValidationInterceptor : IInterceptor {
        /// <summary>
        /// 拦截方法。
        /// </summary>
        /// <param name="invocation">被拦截方法的调用</param>
        public void Intercept(IInvocation invocation) {
            new InputDtoValidator(invocation.Method, invocation.Arguments).Validate();
            invocation.Proceed();
        }
    }
}
