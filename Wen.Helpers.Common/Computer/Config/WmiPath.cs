namespace Wen.Helpers.Common.Computer.Config
{
    public class WmiPath
    {
        /// <summary>
        /// 内存
        /// </summary>
        public const string PhysicalMemory = "Win32_PhysicalMemory";

        /// <summary>
        /// cpu
        /// </summary>
        public const string Processor = "Win32_Processor";

        /// <summary>
        /// 硬盘
        /// </summary>
        public const string DiskDrive = "win32_DiskDrive";

        /// <summary>
        /// 电脑型号
        /// </summary>
        public const string ComputerSystemProduct = "Win32_ComputerSystemProduct";

        /// <summary>
        /// 分辨率
        /// </summary>
        public const string DesktopMonitor = "Win32_DesktopMonitor";

        /// <summary>
        /// 显卡
        /// </summary>
        public const string VideoController = "Win32_VideoController";

        /// <summary>
        /// 操作系统
        /// </summary>
        public const string OperatingSystem = "Win32_OperatingSystem";
    }
}