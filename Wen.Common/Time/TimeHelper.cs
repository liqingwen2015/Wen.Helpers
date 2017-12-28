#region namespaces

using System;

#endregion

namespace Wen.Common.Time
{
    /// <summary>
    /// 时间助手
    /// </summary>
    public static class TimeHelper
    {
        /// <summary>
        /// DateTime 时间格式转换为 13 位带毫秒的 Unix 时间戳
        /// </summary>
        /// <param name="time">需转换的时间，null 时取当前的时间（DateTime.Now）</param>
        /// <returns>Unix 时间戳格式</returns>
        public static long ToTimeStampLong(DateTime? time = null)
        {
            return (long) ToTimeStamp(time);
        }

        /// <summary>
        /// DateTime 时间格式转换为 10 位不带毫秒的 Unix 时间戳
        /// </summary>
        /// <param name="time">需转换的时间，null 时取当前的时间（DateTime.Now）</param>
        /// <returns>Unix 时间戳格式</returns>
        public static int ToTimeStampInt(DateTime? time = null)
        {
            return (int) ToTimeStamp(time);
        }

        /// <summary>
        /// 时间戳转为 C# 格式时间
        /// </summary>
        /// <param name="timeStamp">Unix 时间戳格式</param>
        /// <returns>C# 格式时间</returns>
        public static DateTime ToDateTime(string timeStamp)
        {
            var isSucceed = long.TryParse(timeStamp + "0000000", out var convertedTimeStamp);

            if (!isSucceed) return default(DateTime);

            var timeSpanValue = new TimeSpan(convertedTimeStamp);
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            return startTime.Add(timeSpanValue);
        }

        #region private method

        /// <summary>
        ///  转换成时间戳
        /// </summary>
        /// <param name="time">需转换的时间，null 时取当前的时间（DateTime.Now）</param>
        /// <returns></returns>
        private static double ToTimeStamp(DateTime? time = null)
        {
            if (time == null)
                time = DateTime.Now;

            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            return (time.Value - startTime).TotalSeconds;
        }

        #endregion private method
    }
}