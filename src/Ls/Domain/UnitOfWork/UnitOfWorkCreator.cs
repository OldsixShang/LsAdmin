using System.Runtime.Remoting.Messaging;

namespace Ls.Domain.UnitOfWork {
    /// <summary>
    /// 工作单元生成器。
    /// </summary>
    public static class UnitOfWorkCreator {
        private const string KEY = "Ls";

        /// <summary>
        /// 设置或获取当前上下文的工作单元。
        /// </summary>
        public static IUnitOfWork UnitOfWork {
            get {
                IUnitOfWork unitOfWork = CallContext.LogicalGetData(KEY) as IUnitOfWork;
                if (unitOfWork == null) {
                    return null;
                }
                if (unitOfWork.IsDisposed) {
                    return null;
                }
                return unitOfWork;
            }

            set {
                IUnitOfWork unitOfWork = CallContext.LogicalGetData(KEY) as IUnitOfWork;
                if (unitOfWork != null) {
                    if (unitOfWork == value) {
                        return;
                    }
                    CallContext.LogicalSetData(KEY, null);
                }
                if (value == null) {
                    return;
                }
                CallContext.LogicalSetData(KEY, value);
            }
        }

    }
}
