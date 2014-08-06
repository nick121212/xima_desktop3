using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Resources;

namespace XIMALAYA.PCDesktop.Tools.Converter
{
    /// <summary>
    /// 图片转换
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class ImgConverter : IValueConverter
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
            //throw new NotImplementedException();
            //string strSource = "/LuckyLottery;component/person/" + value + ".jpg";
            //return strSource;
            BitmapImage img = new BitmapImage();
            //若要原始文件的站点，可以调用 Application 类的 GetRemoteStream 方法，同时传递标识原始文件的所需站点的 pack URI。 GetRemoteStream 将返回一个 StreamResourceInfo 对象，该对象将原始文件的该站点作为 Stream 公开，并描述其内容类型。
            StreamResourceInfo info = Application.GetRemoteStream(new Uri(value.ToString(), UriKind.RelativeOrAbsolute));
            img.BeginInit();
            //img.UriSource = new Uri(value.ToString(), UriKind.Relative);
            img.StreamSource = info.Stream;
            img.EndInit();
            return img;
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
            throw new NotImplementedException();
        }
    }
}
