using Ls.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ls.Application.Dto;

namespace Ls.Domain.Repositories
{
    /// <summary>
    /// 仓储基类。
    /// </summary>
    /// <typeparam name="TEntity">实体类型参数</typeparam>
    /// <typeparam name="TId">实体标识类型</typeparam>
    public abstract class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        /// <summary>
        /// 添加实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        public abstract void Add(TEntity entity);

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        public abstract void Delete(TEntity entity);

        /// <summary>
        /// 修改实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        public abstract void Update(TEntity entity);

        /// <summary>
        /// 获取实体的表对象。
        /// </summary>
        /// <param name="includePaths"></param>
        /// <returns></returns>
        public abstract IQueryable<TEntity> GetTable(params string[] includePaths);


        public abstract IQueryable<TEntity> Table { get; }
        /// <summary>
        /// 直接执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="doNotEnsureTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public abstract int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        /// <summary>
        /// 执行SQL查询。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">匿名类型的参数</param>
        /// <returns>返回动态类型的查询结果。</returns>
        public abstract dynamic ExecuteSqlCommand(string sql, object parameters);

        /// <summary>
        /// 根据实体标识获取实体对象。
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns>返回实体对象。</returns>
        public TEntity Get(TId id)
        {
            return GetTable().SingleOrDefault(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 根据条件获取实体对象。
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回实体对象</returns>
        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return GetTable().SingleOrDefault(predicate);
        }

        /// <summary>
        /// 获取所有实体。
        /// </summary>
        /// <returns>返回实体列表。</returns>
        public List<TEntity> GetAll()
        {
            return GetTable().ToList();
        }

        /// <summary>
        /// 根据条件获取实体列表。
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="sortPredicate">排序规则</param>
        /// <param name="sortOrder">排序顺序</param>
        /// <returns>返回实体列表。</returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder = SortOrder.Default)
        {
            switch (sortOrder)
            {
                case SortOrder.Default:
                    return GetTable().Where(predicate).ToList();
                case SortOrder.Asc:
                    return GetTable().Where(predicate).OrderBy(sortPredicate).ToList();
                case SortOrder.Desc:
                    return GetTable().Where(predicate).OrderByDescending(sortPredicate).ToList();
                default:
                    return GetTable().Where(predicate).ToList();
            }
        }

        /// <summary>
        /// 根据条件获取实体列表。
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="sortPredicate">排序规则</param>
        /// <param name="skip">跳过的记录数量</param>
        /// <param name="count">获取的记录数量</param>
        /// <param name="sortOrder">排序顺序</param>
        /// <returns>返回实体列表。</returns>
        public IPagedList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> sortPredicate, int skip, int count, SortOrder sortOrder = SortOrder.Default)
        {
            switch (sortOrder)
            {
                case SortOrder.Default:
                    return GetTable().Where(predicate).ToPagedList(skip, count);
                case SortOrder.Asc:
                    return GetTable().Where(predicate).OrderBy(sortPredicate).ToPagedList(skip, count);
                case SortOrder.Desc:
                    return GetTable().Where(predicate).OrderByDescending(sortPredicate).ToPagedList(skip, count);
                default:
                    return GetTable().Where(predicate).ToPagedList(skip, count);
            }
        }

        /// <summary>
        /// 统计符合条件的记录数。
        /// </summary>
        /// <param name="predicate">查询条件，默认为 null</param>
        /// <returns>返回记录数。</returns>
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return GetTable().Count();
            }
            return GetTable().Count(predicate);
        }

        /// <summary>
        /// 根据实体标识生成查询表达式。
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns>返回实体查询表达式。</returns>
        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TId id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TId))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }


    }
}
