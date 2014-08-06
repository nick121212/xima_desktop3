using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace XIMALAYA.PCDesktop.Tools.Converter
{
    /// <summary>
    /// 时间转换 毫秒转换时间
    /// </summary>
    [ValueConversion(typeof(double), typeof(string))]
    public class TimeSpanConverter : IValueConverter
    {
        /// <summary>
        /// 转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double timeSpan = double.Parse(value.ToString());

            TimeSpan ts = TimeSpan.FromMilliseconds(timeSpan);

            int hour = ts.Hours;
            //计算分钟,用毫秒总数减去小时乘以(1000*60*24)后,除以(1000*60),再去掉小数点
            int min = ts.Minutes;
            //同上
            int sec = ts.Seconds;
            //int msec = (int)timeSpan - hour * (1000 * 60 * 24) - min * (1000 * 60) - sec * 1000;
            //拼接字符串
            string timeString = string.Empty;

            if (hour > 0)
            {
                timeString = string.Concat(new string[] { timeString, hour.ToString("d2"), ":" });
            }
            timeString = string.Concat(new string[] { timeString, min.ToString("d2"), ":" });
            timeString = string.Concat(new string[] { timeString, sec.ToString("d2"), "" });

            return timeString;
        }
        /// <summary>
        /// 逆转换
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
            //throw new NotImplementedException();
        }

    }
}
