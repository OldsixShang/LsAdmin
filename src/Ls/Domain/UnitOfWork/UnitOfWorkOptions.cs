using System;
using System.Transactions;

namespace Ls.Domain.UnitOfWork {
    /// <summary>
    /// 工作单元选项。
    /// </summary>
    public class UnitOfWorkOptions {
        /// <summary>
        /// 是否对工作单元开启事务。
        /// </summary>
        public bool IsTransactional { get; set; }

        /// <summary>
        /// 超时时间。
        /// </summary>
        public TimeSpan Timeout { get; set; }

        /// <summary>
        /// 事务隔离级别。
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 提供用于创建事务范围的附加选项。
        /// </summary>
        public TransactionScopeOption TransactionScopeOption { get; set; }  
    }
}
