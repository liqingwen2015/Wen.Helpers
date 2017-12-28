using System;
using Wen.Common.Redis2.Factory;
using Wen.Helpers.Common.Redis2.Product;

namespace Wen.Helpers.Common.Redis2.Factory
{
    public class RedisProviderFactory : RedisAbstractFactory
    {
        public override RedisKeyManager GetKeyManager()
        {
            throw new NotImplementedException();
        }

        public override RedisProduct GetProduct(string productName)
        {
            throw new NotImplementedException();
        }

        public override RedisProvider GetProvider()
        {
            return new RedisProvider();
        }
    }
}