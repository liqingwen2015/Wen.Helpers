using Newtonsoft.Json;

namespace Wen.Common.Extension
{
    /// <summary>
    /// 泛型扩展方法
    /// </summary>
    public static class GenericExtendMethods
    {
        /// <summary>
        /// 转换成 Json 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T self)
        {
            return JsonConvert.SerializeObject(self);
        }
    }
}