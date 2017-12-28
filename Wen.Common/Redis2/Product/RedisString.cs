#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Wen.Helpers.Common.Redis2.Product
{
    /// <summary>
    /// Redis 字符串
    /// </summary>
    public partial class RedisString : RedisProduct
    {
        /// <summary>
        /// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.StringSet(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 保存多个 Key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public bool Set(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(AddKeyPrefix(x.Key), x.Value));
            return Db.StringSet(pairs.ToArray());
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public string Get(string redisKey, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.StringGet(redisKey);
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(redisValue);
            return Db.StringSet(redisKey, json, expiry);
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public T Get<T>(string redisKey, TimeSpan? expiry = null)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Deserialize<T>(Db.StringGet(redisKey));
        }

        #region ctor

        /// <summary>
        /// 默认构造函数（所有选项取默认值）
        /// </summary>
        public RedisString() : this(null, -1, null)
        {
        }

        public RedisString(int db = -1) : this(null, db)
        {
        }

        public RedisString(string connectionString = null, int db = -1) :
            this(connectionString, db, null)
        {
        }

        public RedisString(string connectionString = null, int db = -1, string keyPrefix = null) :
            base(connectionString, db, keyPrefix)
        {
        }

        #endregion ctor
    }
}