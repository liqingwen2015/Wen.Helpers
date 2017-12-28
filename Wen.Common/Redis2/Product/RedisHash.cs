#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#endregion

namespace Wen.Helpers.Common.Redis2.Product
{
    public class RedisHash : RedisProduct
    {
        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool Exists(string redisKey, string hashField)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.HashExists(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public bool Delete(string redisKey, string hashField)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.HashDelete(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public long Delete(string redisKey, IEnumerable<string> hashFields)
        {
            redisKey = AddKeyPrefix(redisKey);
            var fields = hashFields.Select(x => (RedisValue) x);

            return Db.HashDelete(redisKey, fields.ToArray());
        }

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Set(string redisKey, string hashField, string value)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.HashSet(redisKey, hashField, value);
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        public void Set(string redisKey, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            redisKey = AddKeyPrefix(redisKey);
            var entries = hashFields.Select(x => new HashEntry(x.Key, x.Value));

            Db.HashSet(redisKey, entries.ToArray());
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public string Get(string redisKey, string hashField)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.HashGet(redisKey, hashField);
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public IEnumerable<string> Get(string redisKey, IEnumerable<string> hashFields)
        {
            redisKey = AddKeyPrefix(redisKey);
            var fields = hashFields.Select(x => (RedisValue) x);

            return ConvertToStrings(Db.HashGet(redisKey, fields.ToArray()));
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public IEnumerable<string> Keys(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return ConvertToStrings(Db.HashKeys(redisKey));
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public IEnumerable<string> Values(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return ConvertToStrings(Db.HashValues(redisKey));
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public bool Set<T>(string redisKey, string hashField, T redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(redisValue);

            return Db.HashSet(redisKey, hashField, json);
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public T Get<T>(string redisKey, string hashField)
        {
            redisKey = AddKeyPrefix(redisKey);

            return Deserialize<T>(Db.HashGet(redisKey, hashField));
        }

        #region ctor

        /// <summary>
        /// 默认构造函数（所有选项取默认值）
        /// </summary>
        public RedisHash() : this(null, -1, null, null)
        {
        }

        public RedisHash(int db = -1) : this(null, db)
        {
        }

        public RedisHash(string connectionString = null, int db = -1) :
            this(connectionString, db, null, null)
        {
        }

        public RedisHash(string connectionString = null, int db = -1, string keyPrefix = null) :
            this(connectionString, db, keyPrefix, null)
        {
        }

        public RedisHash(string connectionString = null, int db = -1, string keyPrefix = null,
            object asyncState = null) :
            base(connectionString, db, keyPrefix, asyncState)
        {
        }

        #endregion ctor

        #region async

        /// <summary>
        /// 判断该字段是否存在 hash 中
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashExistsAsync(string redisKey, string hashField)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.HashExistsAsync(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<bool> HashDeleteAsync(string redisKey, string hashField)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.HashDeleteAsync(redisKey, hashField);
        }

        /// <summary>
        /// 从 hash 中移除指定字段
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public async Task<long> HashDeleteAsync(string redisKey, IEnumerable<string> hashFields)
        {
            redisKey = AddKeyPrefix(redisKey);
            var fields = hashFields.Select(x => (RedisValue) x);

            return await Db.HashDeleteAsync(redisKey, fields.ToArray());
        }

        /// <summary>
        /// 在 hash 设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync(string redisKey, string hashField, string value)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.HashSetAsync(redisKey, hashField, value);
        }

        /// <summary>
        /// 在 hash 中设定值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        public async Task HashSetAsync(string redisKey, IEnumerable<KeyValuePair<string, string>> hashFields)
        {
            redisKey = AddKeyPrefix(redisKey);
            var entries = hashFields.Select(x => new HashEntry(AddKeyPrefix(x.Key), x.Value));
            await Db.HashSetAsync(redisKey, entries.ToArray());
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<string> HashGetAsync(string redisKey, string hashField)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.HashGetAsync(redisKey, hashField);
        }

        /// <summary>
        /// 在 hash 中获取值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashFields"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> HashGetAsync(string redisKey, IEnumerable<string> hashFields,
            string value)
        {
            redisKey = AddKeyPrefix(redisKey);
            var fields = hashFields.Select(x => (RedisValue) x);

            return ConvertToStrings(await Db.HashGetAsync(redisKey, fields.ToArray()));
        }

        /// <summary>
        /// 从 hash 返回所有的字段值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> HashKeysAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return ConvertToStrings(await Db.HashKeysAsync(redisKey));
        }

        /// <summary>
        /// 返回 hash 中的所有值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> HashValuesAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return ConvertToStrings(await Db.HashValuesAsync(redisKey));
        }

        /// <summary>
        /// 在 hash 设定值（序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public async Task<bool> HashSetAsync<T>(string redisKey, string hashField, T value)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(value);
            return await Db.HashSetAsync(redisKey, hashField, json);
        }

        /// <summary>
        /// 在 hash 中获取值（反序列化）
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="hashField"></param>
        /// <returns></returns>
        public async Task<T> HashGetAsync<T>(string redisKey, string hashField)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Deserialize<T>(await Db.HashGetAsync(redisKey, hashField));
        }

        #endregion async
    }
}