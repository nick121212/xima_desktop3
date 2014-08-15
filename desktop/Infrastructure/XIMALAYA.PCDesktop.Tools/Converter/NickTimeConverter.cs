using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using XIMALAYA.PCDesktop.Tools.Extension;

namespace XIMALAYA.PCDesktop.Tools.Converter
{
    [ValueConversion(typeof(long), typeof(string))]
    public class NickTimeConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            long unix = long.Parse(value.ToString());
            var dt = unix.DateFormatToNiceTime();

            return dt.DateFormatToNiceTime();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }

        #endregion
    }
}
