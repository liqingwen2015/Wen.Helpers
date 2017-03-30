#region

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace Wen.Helpers.Common
{
    /// <summary>
    /// 安全助手
    /// </summary>
    public sealed class SecurityHelper
    {
        private static readonly byte[] IvBytes = {0x01, 0x23, 0x45, 0x67, 0x89, 0xAB, 0xCD, 0xEF};

        #region 通用加密算法

        /// <summary>
        /// 哈希加密算法
        /// </summary>
        /// <param name="hashAlgorithm"> 所有加密哈希算法实现均必须从中派生的基类 </param>
        /// <param name="input"> 待加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        private static string HashEncrypt(HashAlgorithm hashAlgorithm, string input, Encoding encoding)
        {
            var data = hashAlgorithm.ComputeHash(encoding.GetBytes(input));

            return BitConverter.ToString(data).Replace("-", "");
        }

        /// <summary>
        /// 验证哈希值
        /// </summary>
        /// <param name="hashAlgorithm"> 所有加密哈希算法实现均必须从中派生的基类 </param>
        /// <param name="unhashedText"> 未加密的字符串 </param>
        /// <param name="hashedText"> 经过加密的哈希值 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        private static bool VerifyHashValue(HashAlgorithm hashAlgorithm, string unhashedText, string hashedText,
            Encoding encoding)
        {
            return string.Equals(HashEncrypt(hashAlgorithm, unhashedText, encoding), hashedText,
                StringComparison.OrdinalIgnoreCase);
        }

        #endregion 通用加密算法

        #region 哈希加密算法

        #region MD5 算法

        /// <summary>
        /// MD5 加密
        /// </summary>
        /// <param name="input"> 待加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string Md5Encrypt(string input, Encoding encoding)
        {
            return HashEncrypt(MD5.Create(), input, encoding);
        }

        /// <summary>
        /// 验证 MD5 值
        /// </summary>
        /// <param name="input"> 未加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static bool VerifyMd5Value(string input, Encoding encoding)
        {
            return VerifyHashValue(MD5.Create(), input, Md5Encrypt(input, encoding), encoding);
        }

        #endregion MD5 算法

        #region SHA1 算法

        /// <summary>
        /// SHA1 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string Sha1Encrypt(string input, Encoding encoding)
        {
            return HashEncrypt(SHA1.Create(), input, encoding);
        }

        /// <summary>
        /// 验证 SHA1 值
        /// </summary>
        /// <param name="input"> 未加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static bool VerifySha1Value(string input, Encoding encoding)
        {
            return VerifyHashValue(SHA1.Create(), input, Sha1Encrypt(input, encoding), encoding);
        }

        #endregion SHA1 算法

        #region SHA256 算法

        /// <summary>
        /// SHA256 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string Sha256Encrypt(string input, Encoding encoding)
        {
            return HashEncrypt(SHA256.Create(), input, encoding);
        }

        /// <summary>
        /// 验证 SHA256 值
        /// </summary>
        /// <param name="input"> 未加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static bool VerifySha256Value(string input, Encoding encoding)
        {
            return VerifyHashValue(SHA256.Create(), input, Sha256Encrypt(input, encoding), encoding);
        }

        #endregion SHA256 算法

        #region SHA384 算法

        /// <summary>
        /// SHA384 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string Sha384Encrypt(string input, Encoding encoding)
        {
            return HashEncrypt(SHA384.Create(), input, encoding);
        }

        /// <summary>
        /// 验证 SHA384 值
        /// </summary>
        /// <param name="input"> 未加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static bool VerifySha384Value(string input, Encoding encoding)
        {
            return VerifyHashValue(SHA256.Create(), input, Sha384Encrypt(input, encoding), encoding);
        }

        #endregion SHA384 算法

        #region SHA512 算法

        /// <summary>
        /// SHA512 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string Sha512Encrypt(string input, Encoding encoding)
        {
            return HashEncrypt(SHA512.Create(), input, encoding);
        }

        /// <summary>
        /// 验证 SHA512 值
        /// </summary>
        /// <param name="input"> 未加密的字符串 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static bool VerifySha512Value(string input, Encoding encoding)
        {
            return VerifyHashValue(SHA512.Create(), input, Sha512Encrypt(input, encoding), encoding);
        }

        #endregion SHA512 算法

        #region HMAC-MD5 加密

        /// <summary>
        /// HMAC-MD5 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string HmacMd5Encrypt(string input, string key, Encoding encoding)
        {
            return HashEncrypt(new HMACMD5(encoding.GetBytes(key)), input, encoding);
        }

        #endregion HMAC-MD5 加密

        #region HMAC-SHA1 加密

        /// <summary>
        /// HMAC-SHA1 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string HmacSha1Encrypt(string input, string key, Encoding encoding)
        {
            return HashEncrypt(new HMACSHA1(encoding.GetBytes(key)), input, encoding);
        }

        #endregion HMAC-SHA1 加密

        #region HMAC-SHA256 加密

        /// <summary>
        /// HMAC-SHA256 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string HmacSha256Encrypt(string input, string key, Encoding encoding)
        {
            return HashEncrypt(new HMACSHA256(encoding.GetBytes(key)), input, encoding);
        }

        #endregion HMAC-SHA256 加密

        #region HMAC-SHA384 加密

        /// <summary>
        /// HMAC-SHA384 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string HmacSha384Encrypt(string input, string key, Encoding encoding)
        {
            return HashEncrypt(new HMACSHA384(encoding.GetBytes(key)), input, encoding);
        }

        #endregion HMAC-SHA384 加密

        #region HMAC-SHA512 加密

        /// <summary>
        /// HMAC-SHA512 加密
        /// </summary>
        /// <param name="input"> 要加密的字符串 </param>
        /// <param name="key"> 密钥 </param>
        /// <param name="encoding"> 字符编码 </param>
        /// <returns></returns>
        public static string HmacSha512Encrypt(string input, string key, Encoding encoding)
        {
            return HashEncrypt(new HMACSHA512(encoding.GetBytes(key)), input, encoding);
        }

        #endregion HMAC-SHA512 加密

        #endregion 哈希加密算法

        #region 对称加密算法

        #region Des 加解密

        /// <summary>
        /// DES 加密
        /// </summary>
        /// <param name="input"> 待加密的字符串 </param>
        /// <param name="key"> 密钥（8位） </param>
        /// <returns></returns>
        public static string DesEncrypt(string input, string key)
        {
            try
            {
                var keyBytes = Encoding.UTF8.GetBytes(key);
                //var ivBytes = Encoding.UTF8.GetBytes(iv);

                var des = DES.Create();
                des.Mode = CipherMode.ECB; //兼容其他语言的 Des 加密算法
                des.Padding = PaddingMode.Zeros; //自动补 0

                using (var ms = new MemoryStream())
                {
                    var data = Encoding.UTF8.GetBytes(input);

                    using (var cs = new CryptoStream(ms, des.CreateEncryptor(keyBytes, IvBytes), CryptoStreamMode.Write)
                    )
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
            catch
            {
                return input;
            }
        }

        /// <summary>
        /// DES 解密
        /// </summary>
        /// <param name="input"> 待解密的字符串 </param>
        /// <param name="key"> 密钥（8位） </param>
        /// <returns></returns>
        public static string DesDecrypt(string input, string key)
        {
            try
            {
                var keyBytes = Encoding.UTF8.GetBytes(key);
                //var ivBytes = Encoding.UTF8.GetBytes(iv);

                var des = DES.Create();
                des.Mode = CipherMode.ECB; //兼容其他语言的Des加密算法
                des.Padding = PaddingMode.Zeros; //自动补0

                using (var ms = new MemoryStream())
                {
                    var data = Convert.FromBase64String(input);

                    using (var cs = new CryptoStream(ms, des.CreateDecryptor(keyBytes, IvBytes), CryptoStreamMode.Write)
                    )
                    {
                        cs.Write(data, 0, data.Length);

                        cs.FlushFinalBlock();
                    }

                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch
            {
                return input;
            }
        }

        #endregion Des 加解密

        #endregion 对称加密算法

        #region 非对称加密算法

        /// <summary>
        /// 生成 RSA 公钥和私钥
        /// </summary>
        /// <param name="publicKey"> 公钥 </param>
        /// <param name="privateKey"> 私钥 </param>
        public static void GenerateRsaKeys(out string publicKey, out string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }
        }

        /// <summary>
        /// RSA 加密
        /// </summary>
        /// <param name="publickey"> 公钥 </param>
        /// <param name="content"> 待加密的内容 </param>
        /// <returns> 经过加密的字符串 </returns>
        public static string RsaEncrypt(string publickey, string content)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publickey);
            var cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);

            return Convert.ToBase64String(cipherbytes);
        }

        /// <summary>
        /// RSA 解密
        /// </summary>
        /// <param name="privatekey"> 私钥 </param>
        /// <param name="content"> 待解密的内容 </param>
        /// <returns> 解密后的字符串 </returns>
        public static string RsaDecrypt(string privatekey, string content)
        {
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privatekey);
            var cipherbytes = rsa.Decrypt(Convert.FromBase64String(content), false);

            return Encoding.UTF8.GetString(cipherbytes);
        }

        #endregion 非对称加密算法
    }
}