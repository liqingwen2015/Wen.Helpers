#region namespaces

using System;
using System.Collections.Generic;

#endregion

namespace Wen.Helpers.Core.Extend
{
    /// <summary>
    /// 可枚举类型扩展方法
    /// </summary>
    public static class EnumerableExtendMethod
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
    }
}