#region namespaces

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Wen.Common.Db.Attributes;

#endregion

namespace Wen.Common.Db
{
    /// <summary>
    /// SqlHelper
    /// </summary>
    public abstract class SqlHelper
    {
        protected readonly string ConnectionString =
            ConfigurationManager.ConnectionStrings["SqlConnetionString"].ToString();

        /// <summary>
        /// 执行查询并返回 DataTable 数据集
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string sql, CommandType commandType = CommandType.Text,
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
        public int ExecuteNonQuery(string sql, CommandType commandType = CommandType.Text,
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
        /// 返回结果集中的第一行第一列
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string sql, CommandType commandType = CommandType.Text,
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
        /// 执行 Reader 并读取数据转换成集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IList<T> ExecuteReaderToList<T>(string sql, CommandType commandType = CommandType.Text,
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

        /// <summary>
        /// 大批量复制
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        public void BulkCopy<T>(IList<T> data) where T : class
        {
            if (data == null || !data.Any())
                return;

            var type = typeof(T);
            var properties = type.GetProperties();

            using (var dt = new DataTable())
            {
                foreach (var property in properties)
                    dt.Columns.Add(CreatDataColumn(property));

                using (var bulkCopy = new SqlBulkCopy(ConnectionString))
                {
                    var tableName = GenerateTableName(type);
                    bulkCopy.DestinationTableName = tableName;

                    foreach (var item in data)
                        dt.Rows.Add(CreateRow(dt, properties, item));

                    bulkCopy.WriteToServer(dt);
                }
            }
        }

        #region private static method

        /// <summary>
        /// 执行 DataReader
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private SqlDataReader ExecuteDataReader(string sql, CommandType commandType = CommandType.Text,
            params SqlParameter[] parameters)
        {
            var conn = new SqlConnection(ConnectionString);

            var cmd = CreateSqlCommand(conn, sql, commandType, parameters);
            conn.Open();

            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
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
        /// 创建行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <param name="properties"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private static DataRow CreateRow<T>(DataTable dt, IReadOnlyList<PropertyInfo> properties, T item)
        {
            var row = dt.NewRow();

            for (var i = 0; i < properties.Count; i++)
                row[i] = properties[i].GetValue(item);

            return row;
        }

        /// <summary>
        /// 生成表名
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static string GenerateTableName(Type type)
        {
            var firstTableAttribute = type.GetCustomAttributes(typeof(DbTableAttribute), false).FirstOrDefault();
            var tableAttribute = (DbTableAttribute) firstTableAttribute;

            return firstTableAttribute == null ? type.Name : tableAttribute.Name;
        }

        /// <summary>
        /// 创建数据列
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static DataColumn CreatDataColumn(PropertyInfo property)
        {
            var firstColumnAttribute = property.GetCustomAttributes(typeof(DbColumnAttribute), false).FirstOrDefault();

            if (firstColumnAttribute == null)
                return new DataColumn(property.Name, property.PropertyType);

            var columnAttribute = (DbColumnAttribute) firstColumnAttribute;
            return new DataColumn(columnAttribute.Name, property.PropertyType);
        }

        #endregion private static method
    }
}