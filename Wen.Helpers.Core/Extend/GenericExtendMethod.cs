using Newtonsoft.Json;

namespace Wen.Helpers.Extend
{
    /// <summary>
    /// 泛型扩展方法
    /// </summary>
    public static class GenericExtendMethod
    {
        /// <summary>
        /// 转换成 Json 字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToJsonString<T>(this T self)
        {
            return JsonConvert.SerializeObject(self);
        }
    }
}
