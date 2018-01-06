#region namespaces

using System;
using System.Configuration;

#endregion

namespace Wen.Common.Configuration
{
    /// <summary>
    /// 配置文件助手
    /// </summary>
    public static class ConfigurationHelper
    {
        /// <summary>
        /// 获取 App 配置的值
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <returns></returns>
        public static string GetAppSettingValue(string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
                throw new ArgumentException();

            return ConfigurationManager.AppSettings[keyName];
        }
    }
}