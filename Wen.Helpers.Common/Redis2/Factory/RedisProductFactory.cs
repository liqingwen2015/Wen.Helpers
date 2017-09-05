using System;
using System.Collections.Generic;
using System.Linq;
using Wen.Helpers.Common.Extend;
using Wen.Helpers.Common.Redis2.Product;

namespace Wen.Helpers.Common.Redis2.Factory
{
    public class RedisProductFactory : RedisAbstractFactory
    {


        public override RedisKeyManager GetKeyManager()
        {
            throw new NotImplementedException();
        }

        public override RedisProduct GetProduct(string productName)
        {
            return productName.IsEqual("String")
                ? new RedisString(ConnectionString, Db, KeyPrefix)
                : (productName.IsEqual("Hash")
                    ? new RedisHash(ConnectionString, Db, KeyPrefix)
                    : (productName.IsEqual("SortedSet")
                        ? new RedisSortedSet(ConnectionString, Db, KeyPrefix)
                        : (productName.IsEqual("List")
                            ? new RedisList(ConnectionString, Db, KeyPrefix)
                            : default(RedisProduct))));
        }

        public override RedisProvider GetProvider()
        {
            throw new NotImplementedException();
        }
    }
}