#region namespaces

using System;
using System.Web;
using System.Web.Caching;

#endregion

namespace Wen.Helpers.Common.Cache
{
    /// <summary>
    /// 缓存助手
    /// </summary>
    public class CacheHelper
    {
        /// <summary>
        /// 从 Cache 对象检索指定项
        /// </summary>
        /// <param name="key">要检索的缓存项的标识符 </param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return HttpRuntime.Cache[key];
        }

        /// <summary>
        /// 向 Cache 对象中插入对象
        /// </summary>
        /// <param name="key">用于引用该对象的缓存键 </param>
        /// <param name="value">要插入缓存中的对象</param>
        /// <param name="absoluteExpiration"> 所插入对象将到期并被从缓存中移除的时间。 要避免可能的本地时间问题（例如从标准时间改为夏时制），请使System.DateTime.UtcNow 而不是 System.DateTime.Now 作为此参数值。</param>
        public static void Insert(string key, object value, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key, value, null, absoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration,
                CacheItemPriority.Normal, null);
        }

        /// <summary>
        /// 从 Cache 对象移除指定项
        /// </summary>
        /// <param name="key">要移除的缓存项的 System.String 标识符</param>
        /// <returns>从 Cache 移除的项。 如果未找到键参数中的值，则返回 null </returns>
        public static object Remove(string key)
        {
            return HttpRuntime.Cache.Remove(key);
        }
    }
}