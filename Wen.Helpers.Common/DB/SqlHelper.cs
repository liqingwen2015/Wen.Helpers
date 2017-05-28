#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace Wen.Helpers.Common.DB
{
    /// <summary>
    /// SqlHelper
    /// </summary>
    public class SqlHelper
    {
        private static readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["SqlConnetionString"].ToString();

        /// <summary>
        /// 执行查询并返回 DataTable 数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = CreateSqlCommand(conn, sql, commandType, parameters);
                conn.Open();

                var dataSet = new DataSet();
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dataSet);

                return dataSet.Tables[0];
            }
        }

        /// <summary>
        /// 执行非查询语句（增删改）
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = CreateSqlCommand(conn, sql, commandType, parameters);
                conn.Open();

                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 执行 DataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static SqlDataReader ExecuteDataReader(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] parameters)
        {
            var conn = new SqlConnection(ConnectionString);

            var cmd = CreateSqlCommand(conn, sql, commandType, parameters);
            conn.Open();

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 返回结果集中的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                var cmd = CreateSqlCommand(conn, sql, commandType, parameters);
                conn.Open();

                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// 创建 SqlCommand 对象
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private static SqlCommand CreateSqlCommand(SqlConnection connection, string sql,
            CommandType commandType = CommandType.Text,
            params SqlParameter[] parameters)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.CommandType = commandType;
            cmd.Parameters.AddRange(parameters);

            return cmd;
        }

        /// <summary>
        /// 执行 Reader 并读取数据转换成集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static IList<T> ExecuteReaderToList<T>(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] parameters) where T : new()
        {
            var type = typeof(T);
            var props = type.GetProperties();
            var list = new List<T>();

            using (var reader = ExecuteDataReader(sql, commandType, parameters))
            {
                while (reader.Read())
                {
                    var entity = new T();

                    foreach (var propertyInfo in props)
                    {
                        var schemaTable = reader.GetSchemaTable();
                        if (schemaTable == null)
                            return new List<T>();

                        schemaTable.DefaultView.RowFilter = $"ColumnName='{propertyInfo.Name}'";
                        if (schemaTable.DefaultView.Count <= 0) continue;

                        if (!propertyInfo.CanWrite)
                            continue;

                        var val = reader[propertyInfo.Name];

                        if (val != DBNull.Value)
                            propertyInfo.SetValue(entity, val);
                    }

                    list.Add(entity);
                }
            }

            return list;
        }
    }
}