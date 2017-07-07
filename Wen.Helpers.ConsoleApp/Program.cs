
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Wen.Helpers.Common;
using Wen.Helpers.Common.Extend;
using Wen.Helpers.Common.Http;
using Wen.Helpers.Common.Npoi;

namespace Wen.Helpers.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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
