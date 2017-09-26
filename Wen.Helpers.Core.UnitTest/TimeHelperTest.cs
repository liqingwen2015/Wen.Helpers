#region namespaces

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wen.Helpers.Core.Security;
using Wen.Helpers.Core.Time;

#endregion

namespace Wen.Helpers.Core.UnitTest
{
    [TestClass]
    public class TimeHelperTest
    {
        [TestMethod]
        public void ToTimeStampLong()
        {
            Console.WriteLine(TimeHelper.ToTimeStampLong(DateTime.Now));
            var a = SecurityHelper.DefaultEncoding;
        }

        [TestMethod]
        public void ToTimeStampInt()
        {
            Console.WriteLine(TimeHelper.ToTimeStampInt(DateTime.Now));
        }
    }
}