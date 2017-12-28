#region namespaces

using System;
using System.Collections.Generic;

#endregion

namespace Wen.Common.Extension
{
    /// <summary>
    /// 可枚举类型扩展方法
    /// </summary>
    public static class EnumerableExtendMethods
    {
        /// <summary>
        /// 对所有元素进行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="action"></param>
        public static void ForAll<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence)
                action(item);
        }

        /// <summary>
        /// 串联集合的成员，其中在每个成员之间使用指定的分隔符
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string Join<T>(this IEnumerable<T> data, string separator)
        {
            return string.Join(separator, data);
        }
    }
}