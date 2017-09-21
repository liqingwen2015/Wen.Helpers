using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Wen.Helpers.Common.Performance
{
    public static class PerformanceCounterHelper
    {
        /// <summary>
        /// 获取性能计数器类别列表
        /// </summary>
        /// <remarks>
        ///     虽然系统中有很多可用的计数器类别，但与之交互最频繁的可能是“Cache”（缓存）、“Memory”（内存）、
        ///“Objects”（对象）、“PhysicalDisk”（物理磁盘）、“Process”（进程）、“Processor”（处理器）、
        ///“Server”（服务器）、“System”（系统）和“Thread”（线程）等类别
        /// </remarks>
        /// <returns></returns>
        public static IList<string> GetPerformanceCounterCategoryNames()
        {
            return PerformanceCounterCategory.GetCategories().Select(x => x.CategoryName).ToList();
        }
    }
}