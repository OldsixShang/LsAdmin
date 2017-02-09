using Ls.IoC;
using System.Collections.Generic;
using System.Data;

namespace Ls.Domain.Repositories
{
    /// <summary>
    /// 查询处理器接口。
    /// 该组件仅用于查询，不建议使用该组件中执行写库命令。
    /// </summary>
    public interface IQueryHandler : ITransientDependency
    {
        /// <summary>
        /// 执行SQL命令，并返回查询结果。
        /// 该组件仅用于查询，不建议使用该组件中执行写库命令。
        /// </summary>
        /// <param name="connectionStringName">连接字符串名称</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="args">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>返回SQL语句的查询结果。</returns>
        IList<dynamic> Query(string connectionStringName, string sql, object args = null, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL命令，并返回查询结果。
        /// 该组件仅用于查询，不建议使用该组件中执行写库命令。
        /// </summary>
        /// <typeparam name="T">查询结果泛型参数</typeparam>
        /// <param name="connectionStringName">连接字符串名称</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="args">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>返回SQL语句的查询结果。</returns>
        IList<T> Query<T>(string connectionStringName, string sql, object args = null, CommandType commandType = CommandType.Text);

        /// <summary>
        /// 执行SQL命令，并返回多个查询结果。
        /// 该组件仅用于查询，不建议使用该组件中执行写库命令。
        /// </summary>
        /// <param name="connectionStringName">连接字符串名称</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="args">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>返回SQL语句的查询结果。</returns>
        IList<List<dynamic>> QueryMultiple(string connectionStringName, string sql, object args = null, CommandType commandType = CommandType.Text);

    }
}
