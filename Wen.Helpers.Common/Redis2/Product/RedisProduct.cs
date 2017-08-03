#region

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using StackExchange.Redis;

#endregion

namespace Wen.Helpers.Common.Redis2.Product
{
    public abstract class RedisProduct
    {
        #region protected field

        /// <summary>
        /// redis 连接对象
        /// </summary>
        protected static IConnectionMultiplexer ConnMultiplexer;

        /// <summary>
        /// 数据库
        /// </summary>
        protected readonly IDatabase Db;

        #endregion protected field

        #region 属性

        /// <summary>
        /// 默认的 Key 值（用来当作 RedisKey 的前缀）
        /// </summary>
        public static string DefaultKey { get; }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        #endregion 属性

        #region 构造函数

        static RedisProduct()
        {
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["RedisConnectionString"].ConnectionString;
                ConnMultiplexer = RedisConnection.GetConnectionMultiplexer();
                DefaultKey = ConfigurationManager.AppSettings["Redis.DefaultKey"];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected RedisProduct(int db = -1, object asyncState = null)
        {
            Db = ConnMultiplexer.GetDatabase(db, asyncState);
        }

        #endregion 构造函数

        #region protected method

        /// <summary>
        /// 添加 Key 的前缀
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected static string KeyPrefixAdd(string key)
        {
            return $"{DefaultKey}:{key}";
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        protected static IEnumerable<string> ConvertStrings<T>(IEnumerable<T> list) where T : struct
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

        #region 注册事件

        /// <summary>
        /// 添加注册事件
        /// </summary>
        private static void AddRegisterEvent()
        {
            ConnMultiplexer.ConnectionRestored += ConnMultiplexer_ConnectionRestored;
            ConnMultiplexer.ConnectionFailed += ConnMultiplexer_ConnectionFailed;
            ConnMultiplexer.ErrorMessage += ConnMultiplexer_ErrorMessage;
            ConnMultiplexer.ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
            ConnMultiplexer.HashSlotMoved += ConnMultiplexer_HashSlotMoved;
            ConnMultiplexer.InternalError += ConnMultiplexer_InternalError;
            ConnMultiplexer.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
        }

        /// <summary>
        /// 重新配置广播时（通常意味着主从同步更改）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChangedBroadcast)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生内部错误时（主要用于调试）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_InternalError)}: {e.Exception}");
        }

        /// <summary>
        /// 更改集群时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Console.WriteLine(
                $"{nameof(ConnMultiplexer_HashSlotMoved)}: {nameof(e.OldEndPoint)}-{e.OldEndPoint} To {nameof(e.NewEndPoint)}-{e.NewEndPoint}, ");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConfigurationChanged)}: {e.EndPoint}");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ErrorMessage)}: {e.Message}");
        }

        /// <summary>
        /// 物理连接失败时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionFailed)}: {e.Exception}");
        }

        /// <summary>
        /// 建立物理连接时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine($"{nameof(ConnMultiplexer_ConnectionRestored)}: {e.Exception}");
        }

        #endregion 注册事件
    }
}