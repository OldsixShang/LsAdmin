using System;
using Castle.DynamicProxy;
using System.Reflection;

namespace Ls.Domain.UnitOfWork.Interceptor {
    /// <summary>
    /// 工作单元拦截器。
    /// </summary>
    public class UnitOfWorkInterceptor : IInterceptor {
        private IUnitOfWorkProvider _unitOfWorkProvider;

        /// <summary>
        /// 创建<see cref="UnitOfWorkInterceptor"/>类型的对象。
        /// </summary>
        /// <param name="unitOfWorkProvider">工作单元提供者</param>
        public UnitOfWorkInterceptor(IUnitOfWorkProvider unitOfWorkProvider) {
            _unitOfWorkProvider = unitOfWorkProvider;
        }

        /// <summary>
        /// 使用工作单元拦截方法调用。
        /// </summary>
        /// <param name="invocation">方法调用</param>
        public void Intercept(IInvocation invocation)
        {

            var uowAttribute = invocation.MethodInvocationTarget.GetCustomAttribute<UnitOfWorkAttribute>();
            if (uowAttribute != null && !uowAttribute.IsTransactional)
            {
                invocation.Proceed();
                return;
            }
     
            if (_unitOfWorkProvider.Current != null) {
                invocation.Proceed();
                return;
            }
        
            using (IUnitOfWork uow = _unitOfWorkProvider.NewUnitOfWork()) {
                if (uowAttribute == null)
                {
                    uow.Begin();
                }
                else
                {
                    uow.Begin(uowAttribute.CreateUnitOfWorkOptions());
                }
                invocation.Proceed();
                uow.Complete();
            }
        }

    }
}
