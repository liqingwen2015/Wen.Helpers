using System;
using Wen.Helpers.Common.Redis2.Product;

namespace Wen.Helpers.Common.Redis2
{
    public abstract class RedisFactory
    {
        public abstract RedisProduct GetProduct(Type type);

        
    }
}
