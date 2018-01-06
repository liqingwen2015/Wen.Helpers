#region namespaces

using System;
using System.Linq;
using System.Management;
using Wen.Common.Extensions;
using Wen.Helpers.Computer.Config;

#endregion

namespace Wen.Common.Computer
{
    /// <summary>
    /// 计算机信息助手类
    /// </summary>
    public static class ComputerInfoHelper
    {
        /// <summary>
        /// 获取硬盘容量汇总
        /// </summary>
        public static long GetTotalDiskSize()
        {
            var diskDriveManagement = new ManagementClass(WmiPath.DiskDrive);
            var diskDriveManagementInstances = diskDriveManagement.GetInstances();

            return diskDriveManagementInstances.OfType<ManagementObject>()
                .Select(x => Convert.ToInt64(x[ManagementObjectPropertyName.Size]))
                .Aggregate<long, long>(0, (current, diskSize) => diskSize + current);
        }

        public static string GetProcessorInfo()
        {
            ManagementClass mcpu = new ManagementClass("Win32_Processor");
            ManagementObjectCollection mncpu = mcpu.GetInstances();
            string cpuCount = mncpu.Count.ToString();
            string[] cpuHz = new string[mncpu.Count];
            int count = 0;
            ManagementObjectSearcher MySearcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject MyObject in MySearcher.Get())
            {
                cpuHz[count] = MyObject.Properties["CurrentClockSpeed"].Value.ToString();
                count++;
            }

            return $"cpuCount: {cpuCount}; cpuHz:{cpuHz.ToJoinInString(",")}";
        }
    }
}