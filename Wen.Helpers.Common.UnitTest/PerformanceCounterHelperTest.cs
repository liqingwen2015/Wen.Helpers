using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wen.Helpers.Common.Performance;


namespace Wen.Helpers.Common.UnitTest
{
    [TestClass]
    public class PerformanceCounterHelperTest
    {
        [TestMethod]
        public void GetPerformanceCounterCategoryNames()
        {
            var names=PerformanceCounterHelper.GetPerformanceCounterCategoryNames();
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
