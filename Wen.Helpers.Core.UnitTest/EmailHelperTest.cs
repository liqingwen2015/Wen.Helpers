#region namespaces

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wen.Helpers.Core.Email;

#endregion

namespace Wen.Helpers.Core.UnitTest
{
    [TestClass]
    public class EmailHelperTest
    {
        [TestMethod]
        public void SendEmail()
        {
            EmailHelper.SendEmail("943239005@qq.com", "vtisghjoadlqbedb", "liqingwen2013@outlook.com", "测试", "请忽略");
        }
    }
}