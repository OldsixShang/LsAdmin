using Ls.Domain.Entities;
using Ls.Domain.Repositories;
using Ls.Domain.UnitOfWork;
using Ls.EntityFramework.UnitOfWork;
using Ls.Session;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Ls.EntityFramework.Repositories
{
    /// <summary>
    /// EF 仓储基类。
    /// </summary>
    /// <typeparam name="TDbContext"><see cref="DbContext"/>类型参数</typeparam>
    /// <typeparam name="TEntity"><see cref="IEntity{TId}"/>类型参数</typeparam>
    /// <typeparam name="TId">实体类编号类型</typeparam>
    public class EfRepository<TDbContext, TEntity, TId> : Repository<TEntity, TId>
        where TEntity : class, IEntity<TId>
        where TDbContext : DbContext
    {

        public ILsSession LsSession { get; set; }

        protected EfRepository()
        {
            LsSession = NullLsSession.Instance;
        }

        /// <summary>
        /// 获取 DbContext。
        /// </summary>
        protected TDbContext Context { get { return UnitOfWorkCreator.UnitOfWork.GetDbContext<TDbContext>(); } }

        /// <summary>
        /// 添加实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        public override void Add(TEntity entity)
        {
            if (entity is ICreatedTime)
            {
                ((ICreatedTime)entity).CreatedTime = DateTime.Now;
            }
            if (entity is ICreatedBy)
            {
                (entity as ICreatedBy).CreaterId = LsSession.UserId;
            }
            if (entity is ILastUpdateBy)
            {
                (entity as ILastUpdateBy).LastUpdaterId = LsSession.UserId;
            }
            if (entity is ILastUpdatedTime)
            {
                (entity as ILastUpdatedTime).LastUpdatedTime = DateTime.Now;
            }
            Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        public override void Delete(TEntity entity)
        {
            AttachIfNot(entity);
            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
            }
            else
            {
                Context.Entry(entity).State = EntityState.Deleted;
            }
        }

        /// <summary>
        /// 获取数据表。
        /// </summary>
        /// <returns>返回<see cref="IQueryable"/>类型的数据表。</returns>
        public override IQueryable<TEntity> GetTable(params string[] includePaths)
        {
            IQueryable<TEntity> result = Context.Set<TEntity>();
            foreach (var includePath in includePaths)
            {
                result = result.Include(includePath);
            }
            return result;
        }

        public override IQueryable<TEntity> Table
        {
            get
            {
                return Context.Set<TEntity>();
            }
        }

        /// <summary>
        /// 修改实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        public override void Update(TEntity entity)
        {
            if (entity is ILastUpdatedTime)
            {
                (entity as ILastUpdatedTime).LastUpdatedTime = DateTime.Now;
            }
            if (entity is ILastUpdateBy)
            {
                (entity as ILastUpdateBy).LastUpdaterId = LsSession.UserId;
            }
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            DbEntityEntry<TEntity> entry = Context.Entry<TEntity>(entity); 
            if (entity is ICreatedBy)
            {
                entry.Property("CreaterId").IsModified = false;
            }
            if (entity is ICreatedTime)
            {
                entry.Property("CreatedTime").IsModified = false;
            }

        }

        /// <summary>
        /// 将实体对象附加至数据库上下文。
        /// </summary>
        /// <param name="entity">实体对象</param>
        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Context.Set<TEntity>().Local.Contains(entity))
            {
                Context.Set<TEntity>().Attach(entity);
            }
        }

        /// <summary>
        /// 直接执行Sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="doNotEnsureTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this.Context).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this.Context).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = this.Context.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this.Context).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }

        /// <summary>
        /// 执行SQL查询。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">匿名类型的参数</param>
        /// <returns>返回动态类型的查询结果。</returns>
        public override dynamic ExecuteSqlCommand(string sql, object parameters)
        {
            return this.Context.Database.Connection.Query(sql, parameters).ToList();
        }
    }
}
