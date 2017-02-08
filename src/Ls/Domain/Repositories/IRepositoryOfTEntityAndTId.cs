using Ls.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Ls.Application.Dto;

namespace Ls.Domain.Repositories {
    /// <summary>
    /// 仓储接口，指定仓储的基本方法。
    /// </summary>
    /// <typeparam name="TEntity">仓储的实体类型</typeparam>
    /// <typeparam name="TId">仓储实体类型的标识类型</typeparam>
    public interface IRepository<TEntity, TId> : IRepository
        where TEntity : class, IEntity<TId> {
        /// <summary>
        /// 添加实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Add(TEntity entity);

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(TEntity entity);

        /// <summary>
        /// 修改实体。
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(TEntity entity);

        /// <summary>
        /// 获取实体的表对象。
        /// </summary>
        /// <param name="includePaths">包含级联的路径</param>
        /// <returns></returns>
        IQueryable<TEntity> GetTable(params string[] includePaths);

        /// <summary>
        /// 获取所有 IQueryAble 实体
        /// </summary>
        IQueryable<TEntity> Table { get; }

        /// <summary>
        /// 根据实体标识获取实体对象。
        /// </summary>
        /// <param name="id">实体标识</param>
        /// <returns>返回实体对象。</returns>
        TEntity Get(TId id);

        /// <summary>
        /// 根据条件获取实体对象。
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <returns>返回实体对象</returns>
        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 获取所有实体。
        /// </summary>
        /// <returns>返回实体列表。</returns>
        List<TEntity> GetAll();

        /// <summary>
        /// 根据条件获取实体列表。
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="sortPredicate">排序规则</param>
        /// <param name="sortOrder">排序顺序</param>
        /// <returns>返回实体列表。</returns>
        List<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder = SortOrder.Default);

        /// <summary>
        /// 根据条件获取实体列表。
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="sortPredicate">排序规则</param>
        /// <param name="skip">跳过的记录数量</param>
        /// <param name="count">获取的记录数量</param>
        /// <param name="sortOrder">排序顺序</param>
        /// <returns>返回实体列表。</returns>
        IPagedList<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, dynamic>> sortPredicate, int skip, int count, SortOrder sortOrder = SortOrder.Default);

        /// <summary>
        /// 统计符合条件的记录数。
        /// </summary>
        /// <param name="predicate">查询条件，默认为 null</param>
        /// <returns>返回记录数。</returns>
        int Count(Expression<Func<TEntity, bool>> predicate = null);


        /// <summary>
        /// 直接执行Sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="doNotEnsureTransaction"></param>
        /// <param name="timeout"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

        /// <summary>
        /// 执行SQL查询。
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">匿名类型的参数</param>
        /// <returns>返回动态类型的查询结果。</returns>
        dynamic ExecuteSqlCommand(string sql, object parameters);
    }
}
