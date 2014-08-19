using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace XIMALAYA.PCDesktop.Tools.Converter
{
    /// <summary>
    /// 
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    public class ActualSizeFixedConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return value;
            try
            {
                double v = (double)value;

                String parm = (String)parameter;
                String op = null;
                double num = double.NaN;
                if (parm != null)
                {
                    String[] parms = parm.Split(",".ToCharArray(), 2);
                    op = parms[0];
                    if (parms.Length > 1)
                        num = double.Parse(parms[1]);
                }
                if (double.IsNaN(num))
                {
                    return value;
                }
                switch (op)
                {
                    case "+": return v + num;
                    case "-": return v - num;
                    case "*": return v * num;
                    case "/": return v / num;
                    case "P": return Math.Pow(v, num);
                    case "L": return Math.Log(v, num);
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                Console.WriteLine("转换数值出错：原始值：" + value + ",转换参数：" + parameter + ".消息:" + ex.Message);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }

        #endregion
    }
}
