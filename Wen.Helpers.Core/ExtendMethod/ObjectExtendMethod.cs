using Newtonsoft.Json;

namespace Wen.Helpers.ExtendMethod
{
    /// <summary>
    /// 对象扩展方法
    /// </summary>
    public static class ObjectExtendMethod
    {
        /// <summary>
        /// 转换成 Json 字符串
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static string ToJson(this object self)
        {
            return JsonConvert.SerializeObject(self);
        }
    }
}
