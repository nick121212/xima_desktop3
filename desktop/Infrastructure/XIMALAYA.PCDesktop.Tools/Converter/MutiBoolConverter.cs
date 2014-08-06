using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XIMALAYA.PCDesktop.Tools.Converter
{
    /// <summary>
    /// 
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class MutiBoolConverter : IMultiValueConverter
    {

        #region IMultiValueConverter 成员

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool result = false;

            if (values.Length > 1)
            {
                long first = (long)values[0];

                for (int i = 1; i < values.Length; i++)
                {
                    result = first == (long)values[i];

                    if (!result) break;
                }
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }

        #endregion
    }
}
