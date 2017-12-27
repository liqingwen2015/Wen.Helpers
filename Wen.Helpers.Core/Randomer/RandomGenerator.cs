#region namespaces

using System;

#endregion

namespace Wen.Helpers.Randomer
{
    /// <summary>
    /// 随机数生成器
    /// </summary>
    public static class RandomGenerator
    {
        /// <summary>
        /// 字符集合（去掉 0oO 这几个容易混淆的字符）
        /// </summary>
        private const string CharSet = "123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";

        /// <summary>
        /// 特殊字符集合（去掉 0oO 这几个容易混淆的字符）
        /// </summary>
        private const string HasSpecialCharSet = "123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPQRSTUVWXYZ";


        private static readonly Random Random = new Random();

        /// <summary>
        /// 返回一个介于 0.0 和 1.0 之间的随机数
        /// </summary>
        /// <returns> 大于等于 0.0 并且小于 1.0 的双精度浮点数 </returns>
        public static double NextDouble()
        {
            return Random.NextDouble();
        }

        /// <summary>
        /// 返回一个随机字符串
        /// </summary>
        /// <param name="length"> 字符串长度 </param>
        /// <param name="isHasSpecialChar">是否包含特殊字符</param>
        /// <returns></returns>
        public static string NextString(int length, bool isHasSpecialChar = true)
        {
            var arr = new char[length];
            var charSet = isHasSpecialChar ? HasSpecialCharSet : CharSet;

            for (var i = 0; i < length; i++)
            {
                var index = Random.Next(charSet.Length);
                arr[i] = charSet[index];
            }

            return string.Join("", arr);
        }

        #region Next

        /// <summary>
        /// 返回非负随机数
        /// </summary>
        /// <returns> 大于等于零且小于 System.Int32.MaxValue 的 32 位带符号整数 </returns>
        public static int Next()
        {
            return Random.Next();
        }

        /// <summary>
        /// 返回一个指定范围内的随机数
        /// </summary>
        /// <param name="maxValue"> 要生成的随机数的上限（随机数不能取该上限值）。maxValue 必须大于或等于零 </param>
        /// <returns>
        /// 大于等于零且小于 maxValue 的 32 位带符号整数，即：返回值的范围通常包括零但不包括 maxValue。 不过，如果 maxValue 等于零，则返回 maxValue。
        /// </returns>
        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        /// <summary>
        /// 返回一个指定范围内的随机数
        /// </summary>
        /// <param name="minValue"> 返回的随机数的下界（随机数可取该下界值） </param>
        /// <param name="maxValue"> 返回的随机数的上界（随机数不能取该上界值）。 maxValue 必须大于或等于 minValue</param>
        /// <returns>
        /// 一个大于等于 minValue 且小于 maxValue 的 32 位带符号整数，即：返回的值范围包括 minValue 但不包括 maxValue。
        /// 如果 minValue 等于 maxValue，则返回 minValue。
        /// </returns>
        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        #endregion Next
    }
}