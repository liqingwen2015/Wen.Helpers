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
        public void ToTimeStampLong()
        {
            Console.WriteLine(TimeHelper.ToTimeStampLong(DateTime.Now));
            var a=SecurityHelper.DefaultEncoding;
        }

        [TestMethod]
        public void ToTimeStampInt()
        {
            Console.WriteLine(TimeHelper.ToTimeStampInt(DateTime.Now));
        }
    }

}
