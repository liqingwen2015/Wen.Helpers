using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wen.Helpers.Common.Security;
using Wen.Helpers.Common.Time;


namespace Wen.Helpers.Common.UnitTest
{

    [TestClass]
    public class TimeHelperTest
    {
        [TestMethod]
        public void ConvertDateTimeLong()
        {
            Console.WriteLine(TimeHelper.ConvertDateTimeLong(DateTime.Now));
            var a=SecurityHelper.DefaultEncoding;
        }

        [TestMethod]
        public void ConvertDateTimeInt()
        {
            Console.WriteLine(TimeHelper.ConvertDateTimeInt(DateTime.Now));
        }
    }

}
