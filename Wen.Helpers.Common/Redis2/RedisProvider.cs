using System;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Wen.Helpers.Common.Redis2
{
    internal class RedisProvider
    {
        #region 发布订阅

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handle"></param>
        public void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
        {
            var sub = _connMultiplexer.GetSubscriber();
            sub.Subscribe(channel, handle);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public long Publish(RedisChannel channel, RedisValue message)
        {
            var sub = _connMultiplexer.GetSubscriber();
            return sub.Publish(channel, message);
        }

        /// <summary>
        /// 发布（使用序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public long Publish<T>(RedisChannel channel, T message)
        {
            var sub = _connMultiplexer.GetSubscriber();
            return sub.Publish(channel, Serialize(message));
        }

        #region 发布订阅-async

        /// <summary>
        /// 订阅
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="handle"></param>
        public async Task SubscribeAsync(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
        {
            var sub = _connMultiplexer.GetSubscriber();
            await sub.SubscribeAsync(channel, handle);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<long> PublishAsync(RedisChannel channel, RedisValue message)
        {
            var sub = _connMultiplexer.GetSubscriber();
            return await sub.PublishAsync(channel, message);
        }

        /// <summary>
        /// 发布（使用序列化）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<long> PublishAsync<T>(RedisChannel channel, T message)
        {
            var sub = _connMultiplexer.GetSubscriber();
            return await sub.PublishAsync(channel, Serialize(message));
        }

        #endregion 发布订阅-async

        #endregion 发布订阅
    }
}