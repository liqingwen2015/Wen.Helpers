#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Wen.Helpers.Common.Redis2.Product;

#endregion

namespace Wen.Helpers.Common.Redis2
{
    public class RedisString : RedisProduct
    {
        public RedisString(int db = -1, object asyncState = null) 
            : base(db, asyncState)
        {
        }

        /// <summary>
        /// 设置 key 并保存字符串（如果 key 已存在，则覆盖值）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public bool Set(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.StringSet(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 保存多个 Key-value
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public bool Set(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(KeyPrefixAdd(x.Key), x.Value));
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
            redisKey = KeyPrefixAdd(redisKey);
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
            redisKey = KeyPrefixAdd(redisKey);
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
            redisKey = KeyPrefixAdd(redisKey);
            return Deserialize<T>(Db.StringGet(redisKey));
        }

        #region async

        /// <summary>
        /// 保存一个字符串值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.StringSetAsync(redisKey, redisValue, expiry);
        }

        /// <summary>
        /// 保存一组字符串值
        /// </summary>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync(IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            var pairs = keyValuePairs.Select(x => new KeyValuePair<RedisKey, RedisValue>(KeyPrefixAdd(x.Key), x.Value));
            return await Db.StringSetAsync(pairs.ToArray());
        }

        /// <summary>
        /// 获取单个值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<string> GetAsync(string redisKey, string redisValue, TimeSpan? expiry = null)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.StringGetAsync(redisKey);
        }

        /// <summary>
        /// 存储一个对象（该对象会被序列化保存）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<bool> SetAsync<T>(string redisKey, T redisValue, TimeSpan? expiry = null)
        {
            redisKey = KeyPrefixAdd(redisKey);
            var json = Serialize(redisValue);
            return await Db.StringSetAsync(redisKey, json, expiry);
        }

        /// <summary>
        /// 获取一个对象（会进行反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string redisKey, TimeSpan? expiry = null)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Deserialize<T>(await Db.StringGetAsync(redisKey));
        }

        #endregion async
    }
}