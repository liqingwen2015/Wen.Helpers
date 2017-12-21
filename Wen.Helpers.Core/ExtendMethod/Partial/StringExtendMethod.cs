#region namespaces

using System.Text;
using Wen.Helpers.Security;

#endregion

namespace Wen.Helpers.ExtendMethod
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static partial class StringExtendMethod
    {
        #region 字符串加密：MD5（16Bit + 32Bit）、SHA1、SHA2（SHA224、SHA256、SHA384、SHA512）、HMAC 系列

        #region MD5（16Bit + 32Bit）

        /// <summary>
        /// 对字符串进行 MD5 加密（16Bit）
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToMd5On16Bit(this string self, Encoding encoding = null)
        {
            return ToMd5On32Bit(self, encoding).Substring(8, 16);
        }

        /// <summary>
        /// 对字符串进行 MD5 加密（32Bit）
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToMd5On32Bit(this string self, Encoding encoding = null)
        {
            return SecurityHelper.Md5Encrypt(self, encoding);
        }

        #endregion MD5（16Bit + 32Bit）

        #region SHA1 + SHA2

        /// <summary>
        /// 对字符串进行 SHA1 加密
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToSha1(this string self, Encoding encoding = null)
        {
            return SecurityHelper.Sha1Encrypt(self, encoding);
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
        /// <param name="self"> 字符串本身 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToSha256(this string self, Encoding encoding = null)
        {
            return SecurityHelper.Sha256Encrypt(self, encoding);
        }

        /// <summary>
        /// 对字符串进行 SHA384 加密
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToSha384(this string self, Encoding encoding = null)
        {
            return SecurityHelper.Sha384Encrypt(self, encoding);
        }

        /// <summary>
        /// 对字符串进行 SHA512 加密
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToSha512(this string self, Encoding encoding = null)
        {
            return SecurityHelper.Sha512Encrypt(self, encoding);
        }

        #endregion SHA1+SHA2

        #region HMAC 系列（MD5, SHA1, SHA2）

        /// <summary>
        /// 对字符串进行 HMAC-MD5 加密
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacMd5(this string self, string key, Encoding encoding = null)
        {
            return SecurityHelper.HmacMd5Encrypt(self, key, encoding);
        }

        /// <summary>
        /// 对字符串进行 HMAC-SHA1 加密
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacSha1(this string self, string key, Encoding encoding = null)
        {
            return SecurityHelper.HmacSha1Encrypt(self, key, encoding);
        }

        /// <summary>
        /// 对字符串进行 HMAC-SHA256 加密
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacSha256(this string self, string key, Encoding encoding = null)
        {
            return SecurityHelper.HmacSha256Encrypt(self, key, encoding);
        }

        /// <summary>
        /// 对字符串进行 HMAC-SHA384 加密
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacSha384(this string self, string key, Encoding encoding = null)
        {
            return SecurityHelper.HmacSha384Encrypt(self, key, encoding);
        }

        /// <summary>
        /// 对字符串进行 HMAC-SHA512 加密
        /// </summary>
        /// <param name="self"> 字符串本身 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding">编码，为 null 时取默认值</param>
        /// <returns> 加密后的哈希值 </returns>
        public static string ToHmacSha512(this string self, string key, Encoding encoding = null)
        {
            return SecurityHelper.HmacSha512Encrypt(self, key, encoding);
        }

        #endregion HMAC 系列（MD5, SHA1, SHA2）

        #endregion 字符串加密：MD5（16Bit + 32Bit）、SHA1、SHA2（SHA224、SHA256、SHA384、SHA512）、HMAC 系列
    }
}