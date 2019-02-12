using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Common.Dapper
{
    public class DapperHelper
    {
        /// <summary>
        /// DB Connetction String
        /// </summary>
        private static string connectionString = ConfigurationManager.ConnectionStrings["Default"].ToString();


        /// <summary>
        /// Get List With SQL
        /// </summary> 
        public static async Task<IEnumerable<T>> GetListAsync<T>(string sql, object param = null) where T : class, new()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return await conn.QueryAsync<T>(sql, param, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// Get List With SQL
        /// </summary> 
        public static IEnumerable<T> GetList<T>(string sql, object param = null) where T : class, new()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<T>(sql, param, commandType: CommandType.Text);
            }
        }


        /// <summary>
        /// 动态类型 dynamic，或者转成 IDictionary<string, object>
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static dynamic QueryDynamic(string sql, object param = null)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.QuerySingle(sql, param);
            }
        }


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="sql">查询的sql</param>
        /// <param name="param">替换参数</param>
        /// <returns></returns>
        public static IEnumerable<T> Query<T>(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.Query<T>(sql, param);
            }
        }
        /// <summary>
        /// 查询第一个数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryFirst<T>(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.QueryFirst<T>(sql, param);
            }
        }
        /// <summary>
        /// 查询第一个数据没有返回默认值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryFirstOrDefault<T>(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.QueryFirstOrDefault<T>(sql, param);
            }
        }
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingle<T>(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.QuerySingle<T>(sql, param);
            }
        }
        /// <summary>
        /// 查询单条数据没有返回默认值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingleOrDefault<T>(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.QuerySingleOrDefault<T>(sql, param);
            }
        }
        /// <summary>
        /// 查询单条数据没有返回默认值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        //public static T QuerySingleOrDefault<T>(string sql, object param = null)
        //{
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        return con.QuerySingleOrDefault<T>(sql, param);
        //    }
        //}
        /// <summary>
        /// 曾删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int Execute(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.Execute(sql, param);
            }
        }
        /// <summary>
        /// Reader获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.ExecuteReader(sql, param);
            }
        }
        /// <summary>
        /// Scalar获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.ExecuteScalar(sql, param);
            }
        }

        /// <summary>
        /// Scalar获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ExecuteScalarForT<T>(string sql, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.ExecuteScalar<T>(sql, param);
            }
        }

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<T> ExecutePro<T>(string proc, object param = null)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<T> list = con.Query<T>(proc,
                                            param,
                                            null,
                                            true,
                                            null,
                                            CommandType.StoredProcedure).ToList();
                return list;
            }
        }

        /// <summary>
        /// 分页数量
        /// </summary>
        /// <param name="select"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static int GetAllCount(string table, string where, string orderBy, object param = null)
        {
            var sql = $"select 1 as _c from {table} where {where} ";
            string sqlCount = $"SELECT COUNT(1) AS count FROM({sql}) AS _tTable1";
            return (int)ExecuteScalar(sqlCount, param);
        }
        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="select"></param>
        /// <param name="table"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetPagedList<T>(int skipCount, int maxResultCount, string select, string table, string where, string orderBy, object param = null)
        {
            var sql = $"select {select} from {table} where {where} ";
            string sqlPage = string.Format(
                @"SELECT * 
                    FROM (
                        SELECT *, ROW_NUMBER() OVER (ORDER BY {0}) AS _rowNum 
                            FROM ({1}) AS _tTable1
                        ) AS _tTable2
                    WHERE _rowNum BETWEEN {2}+1 AND {2}+{3}", orderBy, sql, skipCount, maxResultCount);
            return Query<T>(sqlPage, param);
        }


        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="sql">查询的sql</param>
        /// <param name="param">替换参数</param>
        /// <returns></returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.Query<TFirst, TSecond, TReturn>(sql, map, param, splitOn: splitOn);
            }
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="sql">查询的sql</param>
        /// <param name="param">替换参数</param>
        /// <returns></returns>
        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, string splitOn = "Id")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.Query<TFirst, TSecond, TThird, TReturn>(sql, map, param, splitOn: splitOn);
            }
        }
    }
}
