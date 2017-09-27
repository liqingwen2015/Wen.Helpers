#region namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

#endregion

namespace Wen.Helpers.Http
{
    /// <summary>
    /// Http 助手
    /// </summary>
    public static class HttpHelper
    {
        /// <summary>
        /// 默认编码方式
        /// </summary>
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP(HttpRequest httpRequest)
        {
            var result = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = httpRequest.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(result))
                result = httpRequest.UserHostAddress;

            return string.IsNullOrEmpty(result) ? "0.0.0.0" : result;
        }

        #region get

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static string Get(string url, Encoding encoding = null)
        {
            return UploadString(url);
        }

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramDictionary">参数字典</param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static string Get(string url, IDictionary<string, string> paramDictionary,
            Encoding encoding = null)
        {
            //参数转字符串
            var paramString = paramDictionary == null
                ? ""
                : paramDictionary.Aggregate("", (current, param) => current + param.Key + "=" + param.Value);

            return Get(url, paramString, encoding);
        }

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramString">参数字符串</param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static string Get(string url, string paramString, Encoding encoding = null)
        {
            return UploadString(url, paramString, encoding);
        }

        #endregion get

        #region get async

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Encoding encoding = null)
        {
            return await UploadStringAsync(url);
        }

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramDictionary">参数字典</param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, IDictionary<string, string> paramDictionary,
            Encoding encoding = null)
        {
            //参数转字符串
            var paramString = paramDictionary == null
                ? ""
                : paramDictionary.Aggregate("", (current, param) => current + param.Key + "=" + param.Value);

            return await GetAsync(url, paramString, encoding);
        }

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramString">参数字符串</param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, string paramString, Encoding encoding = null)
        {
            return await UploadStringAsync(url, paramString, encoding);
        }

        #endregion Get

        #region post

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static string Post(string url, Encoding encoding = null)
        {
            return Post(url, "", encoding);
        }

        /// <summary>
        /// POST 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramDictionary">参数字典</param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static string Post(string url, IDictionary<string, string> paramDictionary,
            Encoding encoding = null)
        {
            //参数转字符串
            var paramString = paramDictionary == null
                ? ""
                : paramDictionary.Aggregate("", (current, param) => current + param.Key + "=" + param.Value);

            return Post(url, paramString, encoding);
        }

        /// <summary>
        /// POST 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramString">参数字符串</param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static string Post(string url, string paramString = "", Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(url)) throw new Exception("url 为空！");

            try
            {
                paramString = paramString ?? "";
                encoding = encoding ?? DefaultEncoding;
                var data = encoding.GetBytes(paramString);

                using (var webClient = new WebClient())
                {
                    //采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var responseData = webClient.UploadData(url, "POST", data); //得到返回字符流

                    return encoding.GetString(responseData); //解码
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion post

        #region post async

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, Encoding encoding = null)
        {
            return await PostAsync(url, "", encoding);
        }

        /// <summary>
        /// POST 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramDictionary">参数字典</param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, IDictionary<string, string> paramDictionary,
            Encoding encoding = null)
        {
            //参数转字符串
            var paramString = paramDictionary == null
                ? ""
                : paramDictionary.Aggregate("", (current, param) => current + param.Key + "=" + param.Value);

            return await PostAsync(url, paramString, encoding);
        }

        /// <summary>
        /// POST 请求
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="paramString">参数字符串</param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string url, string paramString = "", Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(url)) throw new Exception("url 为空！");

            try
            {
                paramString = paramString ?? "";
                encoding = encoding ?? DefaultEncoding;
                var data = encoding.GetBytes(paramString);

                using (var webClient = new WebClient())
                {
                    //采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    var responseData = await webClient.UploadDataTaskAsync(url, "POST", data); //得到返回字符流

                    return encoding.GetString(responseData); //解码
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion post async

        #region private-method

        private static string UploadString(string url, string paramString = null, Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentException("url 为空");

            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] responseData;
                    encoding = encoding ?? DefaultEncoding;

                    if (string.IsNullOrEmpty(paramString))
                    {
                        responseData = webClient.DownloadData(url);
                    }
                    else
                    {
                        var data = encoding.GetBytes(paramString); //编码，尤其是汉字，事先要看下抓取网页的编码方式
                        responseData = webClient.UploadData(url, "GET", data); //得到返回字符流
                    }

                    return encoding.GetString(responseData); //解码
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #region async

        private static async Task<string> UploadStringAsync(string url, string paramString = null,
            Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(url)) throw new ArgumentException("url 为空");

            try
            {
                using (var webClient = new WebClient())
                {
                    byte[] responseData;
                    encoding = encoding ?? DefaultEncoding;

                    if (string.IsNullOrEmpty(paramString))
                    {
                        responseData = await webClient.DownloadDataTaskAsync(url);
                    }
                    else
                    {
                        var data = encoding.GetBytes(paramString); //编码，尤其是汉字，事先要看下抓取网页的编码方式
                        responseData = await webClient.UploadDataTaskAsync(url, "GET", data); //得到返回字符流
                    }

                    return encoding.GetString(responseData); //解码
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion async

        #endregion private-method
    }
}