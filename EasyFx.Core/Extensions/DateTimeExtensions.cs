using System;

namespace EasyFx.Core.Extensions
{
    /// <summary>
    /// DateTime 拓展
    /// </summary>
    public static partial class DateTimeExtensions
    {
        /// <summary>
        /// 13位时间戳
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static long UnixTimestamp(this DateTime time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = time - origin;
            return (long)Math.Floor(diff.TotalMilliseconds);
        }
        /// <summary>
        /// 13位时间戳转时间(utc)
        /// </summary>
        /// <param name="timestamp">时间戳</param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(this long timestamp)
        {
           return DateTimeOffset.FromUnixTimeMilliseconds(timestamp).LocalDateTime;
        }

        /// <summary>
        /// 转化中国时间
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime UtcToChinaTime(this DateTime time)
        {
            return time.AddHours(8);
        }
    }
}