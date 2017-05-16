#region

using System;
using System.Collections.Generic;
using System.Text;
using Wen.Helpers.Common.Security;

#endregion

namespace Wen.Helpers.Common.Extend
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class ExtendUtil
    {
        #region private

        /// <summary>
        /// 根据字符编码类型枚举转换成对应的 encoding 类型
        /// </summary>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns></returns>
        private static Encoding EncodingTypeToEncoding(EncodingType type)
        {
            Encoding encoding;
            switch (type)
            {
                case EncodingType.Ascii:
                    encoding = Encoding.ASCII;
                    break;
                case EncodingType.BigEndianUnicode:
                    encoding = Encoding.BigEndianUnicode;
                    break;
                case EncodingType.Default:
                    encoding = Encoding.Default;
                    break;
                case EncodingType.Unicode:
                    encoding = Encoding.Unicode;
                    break;
                case EncodingType.Utf32:
                    encoding = Encoding.UTF32;
                    break;
                case EncodingType.Utf7:
                    encoding = Encoding.UTF7;
                    break;
                case EncodingType.Utf8:
                    encoding = Encoding.UTF8;
                    break;
                case EncodingType.Gb2312:
                    encoding = Encoding.GetEncoding("GB2312");
                    break;
                case EncodingType.Gb18030:
                    encoding = Encoding.GetEncoding("GB18030");
                    break;
                case EncodingType.None:
                    encoding = Encoding.UTF8;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return encoding;
        }

        #endregion private

        #region string

        /// <summary>
        /// 用指定的 string 对象来与自身进行比较，判断是否具有相同值（忽略大小写）
        /// </summary>
        /// <param name="a"> 要比较的字符串 </param>
        /// <param name="b"> 待比较的字符串 </param>
        /// <returns></returns>
        public static bool IsEqual(this string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        #region 字符串加密：MD5（16Bit + 32Bit）、SHA1、SHA2（SHA224、SHA256、SHA384、SHA512）、HMAC 系列

        #region MD5（16Bit + 32Bit）

        /// <summary>
        /// 对字符串进行 MD5 加密（16Bit）
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="type"> 字符编码类型 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToMd5On16Bit(this string a, EncodingType type = EncodingType.Utf8)
        {
            return ToMd5On32Bit(a, type).Substring(8, 16);
        }

        /// <summary>
        /// 对字符串进行 MD5 加密（32Bit）
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="type"> 字符编码类型 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToMd5On32Bit(this string a, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.Md5Encrypt(a, encoding);
        }

        #endregion MD5（16Bit + 32Bit）

        #region SHA1 + SHA2

        /// <summary>
        /// 对字符串进行 SHA1 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToSha1(this string a, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.Sha1Encrypt(a, encoding);
        }

        ///// <summary>
        ///// 对字符串进行 SHA224 加密（采用 Bouncy Castle 框架）（不支持“中文”字符串）
        ///// </summary>
        ///// <param name="a"> 字符串本身 </param>
        ///// <param name="type"> 字符编码类型枚举 </param>
        ///// <returns> 加密后的哈希值 </returns>
        //public static string ToSha224(this string a, EncodingType type = EncodingType.UTF8)
        //{
        //    //因为 .NET 没有内置 SHA224 算法
        //    //所以采用第三方框架 Bouncy Castle 内的算法
        //    //【注意】经校验：对“数字”和“字母”的字符串加密正常，对“中文”的字符串加密有误

        //    IDigest digest = new Sha224Digest();
        //    var encoding = EncodingTypeToEncoding(type);

        //    digest.BlockUpdate(encoding.GetBytes(a), 0, a.Length);
        //    var data = new byte[digest.GetDigestSize()];
        //    digest.DoFinal(data, 0);

        //    return BitConverter.ToString(data).Replace("-", "");
        //}

        /// <summary>
        /// 对字符串进行 SHA256 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToSha256(this string a, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.Sha256Encrypt(a, encoding);
        }

        /// <summary>
        /// 对字符串进行 SHA384 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToSha384(this string a, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.Sha384Encrypt(a, encoding);
        }

        /// <summary>
        /// 对字符串进行 SHA512 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToSha512(this string a, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.Sha512Encrypt(a, encoding);
        }

        #endregion SHA1+SHA2

        #region HMAC 系列（MD5, SHA1, SHA2）

        /// <summary>
        /// 对字符串进行 HMAC-MD5 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacMd5(this string a, string key, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.HmacMd5Encrypt(a, key, encoding);
        }

        /// <summary>
        /// 对字符串进行 HMAC-SHA1 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacSha1(this string a, string key, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.HmacSha1Encrypt(a, key, encoding);
        }

        /// <summary>
        /// 对字符串进行 HMAC-SHA256 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacSha256(this string a, string key, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.HmacSha256Encrypt(a, key, encoding);
        }

        /// <summary>
        /// 对字符串进行 HMAC-SHA384 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacSha384(this string a, string key, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.HmacSha384Encrypt(a, key, encoding);
        }

        /// <summary>
        /// 对字符串进行 HMAC-SHA512 加密
        /// </summary>
        /// <param name="a"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="type"> 字符编码类型枚举 </param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacSha512(this string a, string key, EncodingType type = EncodingType.Utf8)
        {
            var encoding = EncodingTypeToEncoding(type);

            return SecurityHelper.HmacSha512Encrypt(a, key, encoding);
        }

        #endregion HMAC 系列（MD5, SHA1, SHA2）

        #endregion 字符串加密：MD5（16Bit + 32Bit）、SHA1、SHA2（SHA224、SHA256、SHA384、SHA512）、HMAC 系列

        #endregion string

        #region IEnumerable

        /// <summary>
        /// 对所有元素进行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="action"></param>
        public static void ForAll<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence)
            {
                action(item);
            }
        }

        #endregion IEnumerable
    }
}