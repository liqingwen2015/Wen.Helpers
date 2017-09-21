#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

#endregion

namespace Wen.Helpers.Common.Http
{
    /// <summary>
    /// Http 助手
    /// </summary>
    public class HttpHelper
    {
        private static readonly Encoding DefaultEncoding = Encoding.UTF8;

        /// <summary>
        /// Get 请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="encoding">字符编码类型（默认 UTF8）</param>
        /// <returns></returns>
        public static async Task<string> GetAsync(string url, Encoding encoding = null)
        {
            try
            {
                encoding = encoding ?? DefaultEncoding;

                if (string.IsNullOrEmpty(url))
                {
                    throw new Exception("url 为空！");
                }

                using (var webClient = new WebClient())
                {
                    var responseData = await webClient.DownloadDataTaskAsync(url);
                    var response = encoding.GetString(responseData);

                    return response;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
            if (string.IsNullOrEmpty(url))
            {
                return "";
            }

            if (paramDictionary == null)
            {
                return await GetAsync(url);
            }

            //参数转字符串
            var paramString =
                paramDictionary.Aggregate("", (current, param) => current + param.Key + "=" + param.Value);

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
            if (string.IsNullOrEmpty(url))
            {
                return "";
            }

            if (string.IsNullOrEmpty(paramString))
            {
                return await GetAsync(url);
            }

            try
            {
                encoding = encoding ?? DefaultEncoding;

                using (var webClient = new WebClient())
                {
                    var data = encoding.GetBytes(paramString); //编码，尤其是汉字，事先要看下抓取网页的编码方式
                    var responseData = await webClient.UploadDataTaskAsync(url, "GET", data); //得到返回字符流

                    return encoding.GetString(responseData); //解码
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
            if (string.IsNullOrEmpty(url))
                throw new Exception("url 为空！");

            try
            {
                //参数转字符串
                var paramString = paramDictionary == null
                    ? ""
                    : paramDictionary.Aggregate("", (current, param) => current + param.Key + "=" + param.Value);

                return await PostAsync(url, paramString, encoding);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
            if (string.IsNullOrEmpty(url))
                throw new Exception("url 为空！");

            try
            {
                paramString = paramString ?? "";
                encoding = encoding ?? DefaultEncoding;

                using (var webClient = new WebClient())
                {
                    //编码，尤其是汉字，事先要看下抓取网页的编码方式
                    var data = encoding.GetBytes(paramString);
                    webClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                    //采取POST方式必须加的header，如果改为GET方式的话就去掉这句话即可
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

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIP()
        {
            var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            return string.IsNullOrEmpty(result) ? "0.0.0.0" : result;
        }
    }
}