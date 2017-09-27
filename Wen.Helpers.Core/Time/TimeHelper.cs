#region namespaces

using System;

#endregion

namespace Wen.Helpers.Time
{
    /// <summary>
    /// 时间助手
    /// </summary>
    public static class TimeHelper
    {
        #region private field

        /// <summary>
        /// 开始时间
        /// </summary>
        private static readonly DateTime StartTime;

        #endregion private field

        #region ctor

        static TimeHelper()
        {
            try
            {
                StartTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion ctor

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
            var isSucceed = long.TryParse(timeStamp + "0000000", out long convertedTimeStamp);

            if (!isSucceed) return default(DateTime);

            var timeSpanValue = new TimeSpan(convertedTimeStamp);
            return StartTime.Add(timeSpanValue);
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

            return (time.Value - StartTime).TotalSeconds;
        }

        #endregion private method
    }
}