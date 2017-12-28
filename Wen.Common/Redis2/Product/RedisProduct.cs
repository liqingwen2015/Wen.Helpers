#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

#endregion

namespace Wen.Helpers.Common.Redis2.Product
{
    /// <summary>
    /// Redis 产品
    /// </summary>
    public abstract class RedisProduct
    {
        #region 属性

        /// <summary>
        /// 默认的 Key 值（用来当作 RedisKey 的前缀）
        /// </summary>
        public static string DefaultKeyPrefix { get; set; }

        #endregion 属性

        #region protected field

        /// <summary>
        /// redis 连接对象
        /// </summary>
        protected IConnectionMultiplexer ConnMultiplexer;

        /// <summary>
        /// 数据库
        /// </summary>
        protected IDatabase Db;

        #endregion protected field

        #region 构造函数

        static RedisProduct()
        {
            try
            {
                DefaultKeyPrefix = ConfigurationManager.AppSettings["Redis.DefaultKey"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected RedisProduct(string connectionString = null, int db = -1, string keyPrefix = null)
        {
            if (string.IsNullOrEmpty(keyPrefix))
                DefaultKeyPrefix = keyPrefix;

            ConnMultiplexer = RedisConnection.GetConnectionMultiplexer(connectionString);
            Db = RedisConnection.GetDatabase(connectionString, db);
        }

        #endregion 构造函数

        #region protected method

        /// <summary>
        /// 添加 Key 的前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static string AddKeyPrefix(string key)
        {
            return $"{DefaultKeyPrefix}:{key}";
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        protected static IEnumerable<string> ConvertToStrings<T>(IEnumerable<T> list) where T : struct
        {
            if (list == null) throw new ArgumentNullException(nameof(list));
            return list.Select(x => x.ToString());
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected static byte[] Serialize(object obj)
        {
            if (obj == null)
                return null;

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream())
            {
                binaryFormatter.Serialize(memoryStream, obj);
                var data = memoryStream.ToArray();
                return data;
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected static T Deserialize<T>(byte[] data)
        {
            if (data == null)
                return default(T);

            var binaryFormatter = new BinaryFormatter();
            using (var memoryStream = new MemoryStream(data))
            {
                var result = (T) binaryFormatter.Deserialize(memoryStream);
                return result;
            }
        }

        #endregion protected method
    }
}