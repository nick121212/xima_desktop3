using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIMALAYA.PCDesktop.Tools.Extension
{
    /// <summary>
    /// 日期扩展
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// long转换日期
        /// </summary>
        /// <param name="unix"></param>
        /// <returns></returns>
        public static DateTime DateFormatToNiceTime(this long unix)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));

            TimeSpan toNow = TimeSpan.FromMilliseconds(unix);
            DateTime dtResult = dtStart.Add(toNow);

            return dtResult;
        }
        /// <summary>
        /// 日期类型转换 几天前
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateFormatToNiceTime(this DateTime dt)
        {
            TimeSpan span = DateTime.Now - dt;
            var totalDays = span.TotalDays;
            var totalHours = span.TotalHours;
            var totalSeconds = span.TotalSeconds;
            var totalMinutes = span.TotalMinutes;

            if (totalDays >= 365)
            {
                return string.Format("{0}年前", Math.Floor(totalDays / 365).ToString());
            }
            if (totalDays >= 30)
            {
                return string.Format("{0}月前", Math.Floor(totalDays / 30).ToString());
            }
            if (totalDays >= 1)
            {
                return string.Format("{0}天前", Math.Floor(totalDays).ToString());
            }
            if (totalHours >= 1)
            {
                return string.Format("{0}小时前", Math.Floor(totalHours).ToString());
            }
            if (totalMinutes >= 1)
            {
                return string.Format("{0}分钟前", Math.Floor(totalMinutes).ToString());
            }
            if (totalSeconds >= 30)
            {
                return string.Format("{0}秒前", Math.Floor(totalSeconds).ToString());
            }

            return "刚刚";
        }
    }
}
