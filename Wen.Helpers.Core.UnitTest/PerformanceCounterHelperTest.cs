#region namespaces

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wen.Helpers.Core.Performance;

#endregion

namespace Wen.Helpers.Core.UnitTest
{
    [TestClass]
    public class PerformanceCounterHelperTest
    {
        [TestMethod]
        public void GetPerformanceCounterCategoryNames()
        {
            var names = PerformanceCounterHelper.GetPerformanceCounterCategoryNames();
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}