#region namespaces

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Wen.Helpers.Computer;

#endregion

namespace Wen.Helpers.Core.UnitTest
{
    [TestClass]
    public class ComputerInfoHelperTest
    {
        [TestMethod]
        public void GetTotalDiskSize()
        {
            var size = ComputerInfoHelper.GetTotalDiskSize();

            Console.WriteLine($"{size} KB");
            Console.WriteLine($"{ConvertSize(size)}");
        }


        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ConvertSize(long b)
        {
            if (b.ToString().Length <= 10)
                return GetMB(b);
            if (b.ToString().Length >= 11 && b.ToString().Length <= 12)
                return GetGB(b);
            if (b.ToString().Length >= 13)
                return GetTB(b);
            return string.Empty;
        }

        /// <summary>
        /// 将B转换为TB
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string GetTB(long b)
        {
            for (var i = 0; i < 4; i++)
            {
                b /= 1024;
            }
            return b + " TB";
        }

        /// <summary>
        /// 将B转换为GB
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string GetGB(long b)
        {
            for (var i = 0; i < 3; i++)
            {
                b /= 1024;
            }
            return b + " GB";
        }

        /// <summary>
        /// 将B转换为MB
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        private static string GetMB(long b)
        {
            for (var i = 0; i < 2; i++)
            {
                b /= 1024;
            }
            return b + " MB";
        }
    }
}