using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace XIMALAYA.PCDesktop.Tools.Converter
{
    /// <summary>
    /// 将任意颜色转换为RGB值相同的透明色
    /// </summary>
    public class ToTransparentColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Color.FromArgb(0, ((Color)value).R, ((Color)value).G, ((Color)value).B);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
