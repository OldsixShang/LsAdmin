using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Ls.Domain.Repositories
{
    /// <summary>
    /// 查询处理器。
    /// 该组件仅用于查询，不建议使用该组件中执行写库命令。
    /// </summary>
    public class QueryHandler : IQueryHandler
    {
        #region IQueryHandler Members

        /// <summary>
        /// 执行SQL命令，并返回查询结果。
        /// 该组件仅用于查询，不建议使用该组件中执行写库命令。
        /// </summary>
        /// <param name="connectionStringName">连接字符串名称</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="args">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>返回SQL语句的查询结果。</returns>
        public IList<dynamic> Query(string connectionStringName, string sql, object args = null, CommandType commandType = CommandType.Text)
        {
            List<dynamic> result;
            using (SqlConnection conn = CreateConnection(connectionStringName))
            {
                result = conn.Query(sql, args, commandType: commandType).ToList();
            }
            return result;
        }

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
        public IList<T> Query<T>(string connectionStringName, string sql, object args = null, CommandType commandType = CommandType.Text)
        {
            List<T> result;
            using (SqlConnection conn = CreateConnection(connectionStringName))
            {
                result = conn.Query<T>(sql, args, commandType: commandType).ToList();
            }
            return result;
        }

        /// <summary>
        /// 执行SQL命令，并返回多个查询结果。
        /// 该组件仅用于查询，不建议使用该组件中执行写库命令。
        /// </summary>
        /// <param name="connectionStringName">连接字符串名称</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="args">命令参数</param>
        /// <param name="commandType">命令类型</param>
        /// <returns>返回SQL语句的查询结果。</returns>
        public IList<List<dynamic>> QueryMultiple(string connectionStringName, string sql, object args = null, CommandType commandType = CommandType.Text)
        {
            IList<List<dynamic>> result = new List<List<dynamic>>();
            using (SqlConnection conn = CreateConnection(connectionStringName))
            {
                var temp = conn.QueryMultiple(sql, args, commandType: commandType);
                while (!temp.IsConsumed)
                {
                    result.Add(temp.Read().ToList());
                }
            }
            return result;
        }

        #endregion

        #region Private method

        /// <summary>
        /// 根据传入的连接字符串名称创建数据库连接。
        /// </summary>
        /// <param name="connectionStringName">连接字符串名称</param>
        /// <returns>返回数据库连接。</returns>
        private SqlConnection CreateConnection(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
            {
                throw new Exception("连接字符串名称不能为空。");
            }

            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception(string.Format("未能获取名称为 {0} 的数据库连接。", connectionStringName));
            }
            return new SqlConnection(connectionString);
        }

        #endregion
    }
}
