using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wen.Helpers.Common.Redis2;

namespace Wen.Helpers.Common.UnitTest
{
    [TestClass]
    public class RedisTest
    {
        [TestMethod]
        public void GetRedisObject()
        {
            var redisString = RedisProductFactory.GetRedisObject<RedisString>();
            
        }
    }
}
