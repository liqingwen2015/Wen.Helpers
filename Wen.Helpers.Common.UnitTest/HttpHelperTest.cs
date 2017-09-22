using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using Wen.Helpers.Common.Http;


namespace Wen.Helpers.Common.UnitTest
{

    [TestClass]
    public class HttpHelperTest
    {
        [TestMethod]
        public void Post()
        {
            var postUrl = "http://10.138.30.38:8099/api/test/savecmd?cmd=";
            var data = "start /min " + "\"\"" + " " + "\"C:/test/abc.txt\"";

            ////var data = new { };

            var dict = new Dictionary<string, string> { { "cmd", data } };
            //HttpHelper.Get(postUrl, dict);
            //var result = HttpHelper.Post(postUrl, "123");
            var p=HttpUtility.UrlEncode(data);
            var result=HttpHelper.Get(postUrl+p);
            Console.WriteLine(result);
        }

        private string HttpPost()
        {
            using (var client = new HttpClient())
            {
                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("cmd", "start /min " + "\"\"" + " " + "\"C:/test/abc.txt\""));
                //values.Add(new KeyValuePair<string, string>("thing2 ", "world"));

                var content = new FormUrlEncodedContent(values);

                var response = client.PostAsync("http://10.138.30.38:8099/api/test/savecmd?cmd", content).Result;

                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }

}
