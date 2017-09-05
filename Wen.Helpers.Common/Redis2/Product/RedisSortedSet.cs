#region

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;
using Wen.Helpers.Common.Redis;

#endregion

namespace Wen.Helpers.Common.Redis2.Product
{
    public class RedisSortedSet : RedisProduct
    {
        #region ctor

        /// <summary>
        /// 默认构造函数（所有选项取默认值）
        /// </summary>
        public RedisSortedSet() : this(null, -1, null, null)
        {

        }

        public RedisSortedSet(int db = -1) : this(null, db)
        {

        }

        public RedisSortedSet(string connectionString = null, int db = -1) :
            this(connectionString, db, null)
        {

        }

        public RedisSortedSet(string connectionString = null, int db = -1, string keyPrefix = null) :
            base(connectionString, db, keyPrefix)
        {

        }


        #endregion ctor

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd(string redisKey, string member, double score)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.SortedSetAdd(redisKey, member, score);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public IEnumerable<string> SortedSetRangeByRank(string redisKey, long start = 0L, long stop = -1L,
            OrderType order = OrderType.Ascending)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.SortedSetRangeByRank(redisKey, start, stop, (Order) order).Select(x => x.ToString());
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public long SortedSetLength(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.SortedSetLength(redisKey);
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public bool SortedSetLength(string redisKey, string memebr)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.SortedSetRemove(redisKey, memebr);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public bool SortedSetAdd<T>(string redisKey, T member, double score)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(member);

            return Db.SortedSetAdd(redisKey, json, score);
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public double SortedSetIncrement(string redisKey, string member, double value = 1)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.SortedSetIncrement(redisKey, member, value);
        }

        #region SortedSet-Async

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync(string redisKey, string member, double score)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.SortedSetAddAsync(redisKey, member, score);
        }

        /// <summary>
        /// 在有序集合中返回指定范围的元素，默认情况下从低到高。
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> SortedSetRangeByRankAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return ConvertToStrings(await Db.SortedSetRangeByRankAsync(redisKey));
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <returns></returns>
        public async Task<long> SortedSetLengthAsync(string redisKey)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.SortedSetLengthAsync(redisKey);
        }

        /// <summary>
        /// 返回有序集合的元素个数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="memebr"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetRemoveAsync(string redisKey, string memebr)
        {
            redisKey = AddKeyPrefix(redisKey);
            return await Db.SortedSetRemoveAsync(redisKey, memebr);
        }

        /// <summary>
        /// SortedSet 新增
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public async Task<bool> SortedSetAddAsync<T>(string redisKey, T member, double score)
        {
            redisKey = AddKeyPrefix(redisKey);
            var json = Serialize(member);

            return await Db.SortedSetAddAsync(redisKey, json, score);
        }

        /// <summary>
        /// 增量的得分排序的集合中的成员存储键值键按增量
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="member"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public Task<double> SortedSetIncrementAsync(string redisKey, string member, double value = 1)
        {
            redisKey = AddKeyPrefix(redisKey);
            return Db.SortedSetIncrementAsync(redisKey, member, value);
        }

        #endregion SortedSet-Async
    }
}