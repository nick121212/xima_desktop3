using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XIMALAYA.PCDesktop.Tools.Converter
{
    [ValueConversion(typeof(object), typeof(long))]
    public class BigNumberToSmallConverter : IValueConverter
    {

        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double val = 0L;

            double.TryParse(value.ToString(), out val);
            if (val / 10000 >= 1)
            {
                val = Math.Round((double)(val / 10000), 2);

                val -= 0.05;

                return val.ToString("F1") + "万";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return 0;
        }

        #endregion
    }
}
