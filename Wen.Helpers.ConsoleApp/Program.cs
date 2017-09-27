#region namespaces

using System;
using System.Threading.Tasks;
using Wen.Helpers.Extend;
using Wen.Helpers.Http;

#endregion

namespace Wen.Helpers.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestApi();

            Console.Read();
        }

        private static async Task TestApi()
        {
            //地址
            const string url = "http://www.ipip5.com/today/api.php?type=json";
            //得到结果
            var result = await HttpHelper.GetAsync(url);
            //字符串转 json
            var json = result.ToJson();

            //想要哪个值就点出对应的属性名
            Console.WriteLine(json.today);
        }
    }
}