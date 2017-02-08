using System;
using System.Transactions;

namespace Ls.Domain.UnitOfWork {
    /// <summary>
    /// 工作单元特性，用于应用层方法。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method|AttributeTargets.Interface, AllowMultiple = false)]
    public class UnitOfWorkAttribute : Attribute {
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
        public IsolationLevel IsolationLevel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TransactionScopeOption TransactionScopeOption { get; set; }  

        /// <summary>
        /// 创建<see cref="UnitOfWorkAttribute"/>对象。
        /// </summary>
        public UnitOfWorkAttribute() {
            IsTransactional = true;
            TransactionScopeOption = TransactionScopeOption.Required;
        }

       /// <summary>
        /// IsolationLevel IsolationLevel 
       /// </summary>
       /// <param name="isTransactional"></param>
       /// <param name="transactionScopeOption"></param>
       /// <param name="timeout"></param>
       /// <param name="isolationLevel"></param>
        public UnitOfWorkAttribute(bool isTransactional,
             int timeout = 0,
             TransactionScopeOption transactionScopeOption=TransactionScopeOption.Required,
             IsolationLevel isolationLevel=IsolationLevel.ReadUncommitted)
       {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
            TransactionScopeOption = transactionScopeOption;
            
        }

        ///// <summary>
        ///// 创建<see cref="UnitOfWorkAttribute"/>对象。
        ///// </summary>
        ///// <param name="timeout">超时时间(毫秒)</param>
        //public UnitOfWorkAttribute(int timeout)
        //{
        //    IsTransactional = true;
        //    Timeout = TimeSpan.FromMilliseconds(timeout);
        //}

        ///// <summary>
        ///// 创建<see cref="UnitOfWorkAttribute"/>对象。
        ///// </summary>
        ///// <param name="isolationLevel">事务隔离级别</param>
        //public UnitOfWorkAttribute(IsolationLevel isolationLevel) {
        //    IsTransactional = true;
        //    IsolationLevel = isolationLevel;
        //}

        ///// <summary>
        ///// 创建<see cref="UnitOfWorkAttribute"/>对象。
        ///// </summary>
        ///// <param name="isTransactional">是否开启事务</param>
        ///// <param name="timeout">超时时间</param>
        //public UnitOfWorkAttribute(bool isTransactional, int timeout) {
        //    IsTransactional = isTransactional;
        //    Timeout = TimeSpan.FromSeconds(timeout);
        //}

        ///// <summary>
        ///// 创建<see cref="UnitOfWorkAttribute"/>对象。
        ///// </summary>
        ///// <param name="timeout">超时时间</param>
        ///// <param name="isolationLevel">事务隔离级别</param>
        //public UnitOfWorkAttribute(int timeout, IsolationLevel isolationLevel) {
        //    IsTransactional = true;
        //    Timeout = TimeSpan.FromSeconds(timeout);
        //    IsolationLevel = isolationLevel;
        //}

        /// <summary>
        /// 生成工作单元的选项。
        /// </summary>
        /// <returns>返回工作单元选项。</returns>
        public UnitOfWorkOptions CreateUnitOfWorkOptions() {
            return new UnitOfWorkOptions {
                IsTransactional = IsTransactional,
                Timeout = Timeout,
                IsolationLevel = IsolationLevel,
                TransactionScopeOption = TransactionScopeOption
            };
        }

    }
}