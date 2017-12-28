using System;
using Wen.Helpers.Common.Redis2;
using Wen.Helpers.Common.Redis2.Product;

namespace Wen.Common.Redis2.Factory
{
    public class RedisKeyManagerFactory : RedisAbstractFactory
    {
        protected RedisKeyManagerFactory()
            : this(null)
        {
        }

        protected RedisKeyManagerFactory(int db = -1)
            : this(null, db)
        {
        }

        protected RedisKeyManagerFactory(string connectionString = null, int db = -1, string keyPrefix = "")
            : base(connectionString, db, keyPrefix)
        {
        }

        public override RedisKeyManager GetKeyManager()
        {
            return new RedisKeyManager();
        }

        public override RedisProduct GetProduct(string productName)
        {
            throw new NotImplementedException();
        }

        public override RedisProvider GetProvider()
        {
            throw new NotImplementedException();
        }
    }
}