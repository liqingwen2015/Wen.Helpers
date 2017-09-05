using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Wen.Helpers.Common.Redis2
{
    /// <summary>
    /// Redis 连接对象
    /// </summary>
    public class RedisConnection
    {
        #region private field

        /// <summary>
        /// redis 连接对象缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, IConnectionMultiplexer> RedisConnectionCache =
            new ConcurrentDictionary<string, IConnectionMultiplexer>();

        /// <summary>
        /// redis 连接对象内的数据库缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, IDatabase> RedisDbCache =
            new ConcurrentDictionary<string, IDatabase>();

        #endregion private field

        #region 属性

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        #endregion 属性

        static RedisConnection()
        {
            try
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["RedisConnectionString"].ConnectionString;

                IConnectionMultiplexer connMultiplexer = GetConnection(ConnectionString);
                RedisConnectionCache.TryAdd(ConnectionString, connMultiplexer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static IConnectionMultiplexer GetConnectionMultiplexer(string connectionString = null)
        {
            connectionString = connectionString ?? ConnectionString;
            var isExist = RedisConnectionCache.TryGetValue(connectionString, out IConnectionMultiplexer connMultiplexer);

            if (isExist && connMultiplexer.IsConnected)
            {
                return connMultiplexer;
            }

            connMultiplexer = GetConnection(connectionString);
            RedisConnectionCache[connectionString] = connMultiplexer;

            return connMultiplexer;
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static IDatabase GetDatabase(string connectionString = null, int db = -1)
        {
            connectionString = connectionString ?? ConnectionString;
            var key = "connectionString" + ":" + db;
            var isExist = RedisDbCache.TryGetValue(key, out IDatabase database);

            if (isExist)
            {
                return database;
            }

            database = GetConnectionMultiplexer(connectionString).GetDatabase(db);
            RedisDbCache[key] = database;
            return database;
        }

        #region 注册事件

        /// <summary>
        /// 添加注册事件
        /// </summary>
        private static ConnectionMultiplexer AddRegisterEvent(ConnectionMultiplexer connectionMultiplexer)
        {
            connectionMultiplexer.ConnectionRestored += ConnMultiplexer_ConnectionRestored;
            connectionMultiplexer.ConnectionFailed += ConnMultiplexer_ConnectionFailed;
            connectionMultiplexer.ErrorMessage += ConnMultiplexer_ErrorMessage;
            connectionMultiplexer.ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
            connectionMultiplexer.HashSlotMoved += ConnMultiplexer_HashSlotMoved;
            connectionMultiplexer.InternalError += ConnMultiplexer_InternalError;
            connectionMultiplexer.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;

            return connectionMultiplexer;
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

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static ConnectionMultiplexer GetConnection(string connectionString = null)
        {
            connectionString = connectionString ?? ConnectionString;

            var connect = ConnectionMultiplexer.Connect(connectionString);
            return AddRegisterEvent(connect);
        }
    }
}
