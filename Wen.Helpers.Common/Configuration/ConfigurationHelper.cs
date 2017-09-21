using System;
using System.Configuration;

namespace Wen.Helpers.Common.Configuration
{
    public static class ConfigurationHelper
    {
        public static string GetAppSettingValue(string keyName)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                throw new ArgumentException();
            }

            return ConfigurationManager.AppSettings[keyName];
        }
    }
}
