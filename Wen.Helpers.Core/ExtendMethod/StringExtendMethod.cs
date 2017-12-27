#region namespaces

using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;

#endregion

namespace Wen.Helpers.ExtendMethod
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static partial class StringExtendMethod
    {
        /// <summary>
        /// 是否相等，用指定的 string 对象来与自身进行比较，判断是否具有相同值（忽略大小写）
        /// </summary>
        /// <param name="self">要比较的字符串</param>
        /// <param name="value">待比较的字符串</param>
        /// <returns></returns>
        public static bool IsEqual(this string self, string value)
        {
            return string.Equals(self, value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 是否不等，用指定的 string 对象来与自身进行比较，判断是否具有相同值（忽略大小写）
        /// </summary>
        /// <param name="self">要比较的字符串</param>
        /// <param name="value">待比较的字符串</param>
        /// <returns></returns>
        public static bool IsNotEqual(this string self, string value)
        {
            return !self.IsEqual(value);
        }

        /// <summary>
        /// 转换成 Json
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static dynamic ToJson(this string self)
        {
            return JsonConvert.DeserializeObject<dynamic>(self);
        }

        /// <summary>
        /// 转换成 Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T ToJson<T>(this string self)
        {
            return JsonConvert.DeserializeObject<T>(self);
        }

        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="self"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsMatch(this string self, string pattern)
        {
            return !string.IsNullOrEmpty(self) && Regex.IsMatch(self, pattern);
        }

        /// <summary>
        /// 是否匹配
        /// </summary>
        /// <param name="self"></param>
        /// <param name="pattern"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static bool IsMatch(this string self, string pattern, RegexOptions options)
        {
            return !string.IsNullOrEmpty(self) && Regex.IsMatch(self, pattern, options);
        }

        /// <summary>
        /// 是否为空或 null 字符串
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string self)
        {
            return string.IsNullOrEmpty(self);
        }

        /// <summary>
        /// 是否不为空或 null 字符串
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this string self)
        {
            return !self.IsNullOrEmpty();
        }
    }
}