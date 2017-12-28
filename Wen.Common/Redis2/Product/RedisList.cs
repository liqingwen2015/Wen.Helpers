#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Wen.Helpers.Common.Redis2.Product
{
    public class RedisList : RedisProduct
    {
        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string LeftPop(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.ListLeftPop(redisKey);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public string RightPop(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.ListRightPop(redisKey);
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long Remove(string redisKey, string redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.ListRemove(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long RightPush(string redisKey, string redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.ListRightPush(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long LeftPush(string redisKey, string redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.ListLeftPush(redisKey, redisValue);
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public long Length(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.ListLength(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public IEnumerable<string> Range(string redisKey, long start = 0L, long stop = -1L)
        {
            redisKey = AddKeyPrefix(redisKey);
            return ConvertToStrings(Db.ListRange(redisKey, start, stop));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T LeftPop<T>(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Deserialize<T>(Db.ListLeftPop(redisKey));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public T RightPop<T>(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Deserialize<T>(Db.ListRightPop(redisKey));
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long RightPush<T>(string redisKey, T redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.ListRightPush(redisKey, Serialize(redisValue));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public long LeftPush<T>(string redisKey, T redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.ListLeftPush(redisKey, Serialize(redisValue));
        }

        #region ctor

        /// <summary>
        /// 默认构造函数（所有选项取默认值）
        /// </summary>
        public RedisList() : this(null, -1, null, null)
        {
        }

        public RedisList(int db = -1) : this(null, db)
        {
        }

        public RedisList(string connectionString = null, int db = -1) :
            this(connectionString, db, null, null)
        {
        }

        public RedisList(string connectionString = null, int db = -1, string keyPrefix = null) :
            this(connectionString, db, keyPrefix, null)
        {
        }

        public RedisList(string connectionString = null, int db = -1, string keyPrefix = null,
            object asyncState = null) :
            base(connectionString, db, keyPrefix, asyncState)
        {
        }

        #endregion ctor

        #region List-async

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> LeftPopAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.ListLeftPopAsync(redisKey);
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<string> RightPopAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.ListRightPopAsync(redisKey);
        }

        /// <summary>
        /// 移除列表指定键上与该值相同的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> RemoveAsync(string redisKey, string redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.ListRemoveAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> RightPushAsync(string redisKey, string redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.ListRightPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> LeftPushAsync(string redisKey, string redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.ListLeftPushAsync(redisKey, redisValue);
        }

        /// <summary>
        /// 返回列表上该键的长度，如果不存在，返回 0
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> LengthAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.ListLengthAsync(redisKey);
        }

        /// <summary>
        /// 返回在该列表上键所对应的元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> RangeAsync(string redisKey, long start = 0L, long stop = -1L)
        {
            redisKey = AddKeyPrefix(redisKey);
            var query = await Db.ListRangeAsync(redisKey, start, stop);

            return query.Select(x => x.ToString());
        }

        /// <summary>
        /// 移除并返回存储在该键列表的第一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> LeftPopAsync<T>(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Deserialize<T>(await Db.ListLeftPopAsync(redisKey));
        }

        /// <summary>
        /// 移除并返回存储在该键列表的最后一个元素
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<T> RightPopAsync<T>(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Deserialize<T>(await Db.ListRightPopAsync(redisKey));
        }

        /// <summary>
        /// 在列表尾部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> RightPushAsync<T>(string redisKey, T redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.ListRightPushAsync(redisKey, Serialize(redisValue));
        }

        /// <summary>
        /// 在列表头部插入值。如果键不存在，先创建再插入值
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="redisValue"></param>
        /// <returns></returns>
        public async Task<long> LeftPushAsync<T>(string redisKey, T redisValue)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.ListLeftPushAsync(redisKey, Serialize(redisValue));
        }

        #endregion List-async
    }
}