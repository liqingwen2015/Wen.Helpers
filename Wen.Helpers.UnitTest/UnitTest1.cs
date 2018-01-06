using System;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wen.Common.Computer;
using Wen.Common.Randomer;

namespace Wen.Helpers.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var info = ComputerInfoHelper.GetProcessorInfo();
            Console.WriteLine(info);
        }
    }
}
