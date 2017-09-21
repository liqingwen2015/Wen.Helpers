using System;
using System.Linq;
using System.Management;
using Wen.Helpers.Common.Computer.Config;

namespace Wen.Helpers.Common.Computer
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
            try
            {
                var diskDriveManagement = new ManagementClass(WmiPath.DiskDrive);
                var diskDriveManagementInstances = diskDriveManagement.GetInstances();

                return diskDriveManagementInstances.OfType<ManagementObject>()
                    .Select(
                        managementObject => Convert.ToInt64(managementObject[ManagementBaseObjectPropertyName.Size]))
                    .Aggregate<long, long>(0, (current, diskSize) => diskSize + current);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}