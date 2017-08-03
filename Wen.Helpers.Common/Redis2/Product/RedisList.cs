#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wen.Helpers.Common.Redis2.Product;

#endregion

namespace Wen.Helpers.Common.Redis2
{
    public class RedisList : RedisProduct
    {
        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string ListLeftPop(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.ListLeftPop(redisKey);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string ListRightPop(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.ListRightPop(redisKey);
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRemove(string redisKey, string redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.ListRemove(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRightPush(string redisKey, string redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.ListRightPush(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListLeftPush(string redisKey, string redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.ListLeftPush(redisKey, redisValue);
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public long ListLength(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.ListLength(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IEnumerable<string> ListRange(string redisKey, long start = 0L, long stop = -1L)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return ConvertStrings(Db.ListRange(redisKey, start, stop));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T ListLeftPop<T>(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Deserialize<T>(Db.ListLeftPop(redisKey));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T ListRightPop<T>(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Deserialize<T>(Db.ListRightPop(redisKey));
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListRightPush<T>(string redisKey, T redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.ListRightPush(redisKey, Serialize(redisValue));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long ListLeftPush<T>(string redisKey, T redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Db.ListLeftPush(redisKey, Serialize(redisValue));
        }

        #region List-async

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListLeftPopAsync(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.ListLeftPopAsync(redisKey);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> ListRightPopAsync(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.ListRightPopAsync(redisKey);
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRemoveAsync(string redisKey, string redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.ListRemoveAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync(string redisKey, string redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.ListRightPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync(string redisKey, string redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.ListLeftPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> ListLengthAsync(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.ListLengthAsync(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> ListRangeAsync(string redisKey, long start = 0L, long stop = -1L)
        {
            redisKey = KeyPrefixAdd(redisKey);
            var query = await Db.ListRangeAsync(redisKey, start, stop);
            return query.Select(x => x.ToString());
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListLeftPopAsync<T>(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Deserialize<T>(await Db.ListLeftPopAsync(redisKey));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> ListRightPopAsync<T>(string redisKey)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return Deserialize<T>(await Db.ListRightPopAsync(redisKey));
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListRightPushAsync<T>(string redisKey, T redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.ListRightPushAsync(redisKey, Serialize(redisValue));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> ListLeftPushAsync<T>(string redisKey, T redisValue)
        {
            redisKey = KeyPrefixAdd(redisKey);
            return await Db.ListLeftPushAsync(redisKey, Serialize(redisValue));
        }

        #endregion List-async
    }
}