using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace XIMALAYA.PCDesktop.Core.ParamsModel
{
    /// <summary>
    /// long和时间转换
    /// </summary>
    [ValueConversion(typeof(int), typeof(ConditionAlbumType))]
    public class IntToConditaionAlbumTypeConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int val = (int)value;
            ConditionAlbumType rtn = ConditionAlbumType.hot;

            try
            {
                rtn = (ConditionAlbumType)val;
            }
            catch { }

            return rtn;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int rtn = 0;

            rtn = System.Convert.ToInt32(value);

            return rtn;
        }
    }
    /// <summary>
    /// long和时间转换
    /// </summary>
    [ValueConversion(typeof(ConditionAlbumType), typeof(bool))]
    public class ConditaionAlbumTypeToBoolConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            ConditionAlbumType rtn = (ConditionAlbumType)value;
            int param = int.Parse(parameter.ToString());

            return System.Convert.ToInt32(rtn) == param;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return parameter;
        }
    }
}
