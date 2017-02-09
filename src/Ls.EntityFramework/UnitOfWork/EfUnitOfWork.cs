using Ls.Domain.UnitOfWork;
using Ls.IoC;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Transactions;

namespace Ls.EntityFramework.UnitOfWork {
    /// <summary>
    /// 基于 EntityFramework 实现的工作单元。
    /// </summary>
    public class EfUnitOfWork : UnitOfWorkBase, ITransientDependency {
        private TransactionScope _transactionScope;
        private Dictionary<Type, DbContext> _activeDbContexts;
        private IIocManager _iocManager;


        /// <summary>
        /// 创建<see cref="EfUnitOfWork"/>实例。
        /// </summary>
        /// <param name="iocManager">IoC 管理器</param>
        public EfUnitOfWork(IIocManager iocManager) {
            _activeDbContexts = new Dictionary<Type, DbContext>();
            _iocManager = iocManager;
        }


        /// <summary>
        /// 开启工作单元。
        /// </summary>
        /// <param name="options">工作单元选项</param>
        public override void Begin(UnitOfWorkOptions options) {
            Options = options;
            if (options.IsTransactional)
            {
                _transactionScope = new TransactionScope(Options.TransactionScopeOption, Options.Timeout);
            }
        }

        /// <summary>
        /// 提交工作单元。
        /// </summary>
        public override void Complete() {
            try
            {
                SaveChanges();
                if (Options.IsTransactional)
                {
                    _transactionScope.Complete();
                }
            }
            catch (DbEntityValidationException exc)
            {
                var msg = string.Empty;
                foreach (var validationErrors in exc.EntityValidationErrors)
                    foreach (var error in validationErrors.ValidationErrors)
                        msg += string.Format("属性值: {0} 错误: {1}", error.PropertyName, error.ErrorMessage) + Environment.NewLine;
                throw new LsException(msg, exc);
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.InnerException is SqlException)
                {
                    SqlException sqlEx = e.InnerException.InnerException as SqlException;
                    string msg = GetSqlExceptionMessage(sqlEx.Number);
                    throw new LsException("提交数据更新时发生异常：" + msg + Environment.NewLine + sqlEx.Message);
                }
                throw;
            }
        }

        /// <summary>
        /// 由错误码返回指定的自定义SqlException异常信息
        /// </summary>
        /// <param name="number"> </param>
        /// <returns> </returns>
        public static string GetSqlExceptionMessage(int number)
        {
            string msg = string.Empty;
            switch (number)
            {
                case 2:
                    msg = "连接数据库超时，请检查网络连接或者数据库服务器是否正常。";
                    break;
                case 17:
                    msg = "SqlServer服务不存在或拒绝访问。";
                    break;
                case 17142:
                    msg = "SqlServer服务已暂停，不能提供数据服务。";
                    break;
                case 2812:
                    msg = "指定存储过程不存在。";
                    break;
                case 208:
                    msg = "指定名称的表不存在。";
                    break;
                case 4060: //数据库无效。
                    msg = "所连接的数据库无效。";
                    break;
                case 18456: //登录失败
                    msg = "使用设定的用户名与密码登录数据库失败。";
                    break;
                case 547:
                    msg = "外键约束，无法保存数据的变更。";
                    break;
                case 2627:
                    msg = "主键重复，无法插入数据。";
                    break;
                case 2601:
                    msg = "未知错误。";
                    break;
            }
            return msg;
        }

        /// <summary>
        /// 释放工作单元中的 DbContext、事务。
        /// </summary>
        protected override void DisposeUnitOfWork() {
            _iocManager.Release(this);

            foreach (var activeDbContext in _activeDbContexts.Values) {
                activeDbContext.Dispose();
                _iocManager.Release(activeDbContext);
            }

            _activeDbContexts.Clear();

            if (_transactionScope != null) {
                _transactionScope.Dispose();
            }
        }

        /// <summary>
        /// 将数据提交至数据库。
        /// </summary>
        private void SaveChanges() {
            foreach (var activeDbContext in _activeDbContexts.Values) {
                activeDbContext.SaveChanges();
            }
        }

        /// <summary>
        /// 获取或创建工作单元中维护的数据库上下文。
        /// </summary>
        /// <typeparam name="TDbContext"><see cref="DbContext"/>类型参数</typeparam>
        /// <returns>返回<see cref="DbContext"/>类型的对象。</returns>
        internal TDbContext GetOrCreateDbContext<TDbContext>()
            where TDbContext : DbContext {
            DbContext dbContext;
            if (!_activeDbContexts.TryGetValue(typeof(TDbContext), out dbContext)) {
                dbContext = _iocManager.Resolve<TDbContext>();
                _activeDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }
    }
}
