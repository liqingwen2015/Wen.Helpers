using Wen.Helpers.Common.Redis2;
using Wen.Helpers.Common.Redis2.Product;

namespace Wen.Common.Redis2.Factory
{
    /// <summary>
    /// redis 抽象工厂
    /// </summary>
    public abstract class RedisAbstractFactory
    {
        protected RedisAbstractFactory(string connectionString = null, int db = -1, string keyPrefix = "")
        {
            ConnectionString = connectionString;
            Db = db;
            KeyPrefix = keyPrefix;
        }

        protected string ConnectionString { get; }

        protected int Db { get; }

        protected string KeyPrefix { get; }

        /// <summary>
        /// 获取 redis 键管理者
        /// </summary>
        /// <returns></returns>
        public abstract RedisKeyManager GetKeyManager();

        /// <summary>
        /// 获取 redis 产品
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public abstract RedisProduct GetProduct(string productName);

        /// <summary>
        /// 获取 redis 提供者
        /// </summary>
        /// <returns></returns>
        public abstract RedisProvider GetProvider();
    }
}