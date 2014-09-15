using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace XIMALAYA.PCDesktop.Controls
{
    public class ImageAttached : DependencyObject
    {

        public static bool GetIsSetGray(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSetGrayProperty);
        }

        public static void SetIsSetGray(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSetGrayProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsSetGray.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSetGrayProperty =
            DependencyProperty.RegisterAttached("IsSetGray", typeof(bool), typeof(ImageAttached), new PropertyMetadata(false, OnIsSetGrayChanged));

        private static void OnIsSetGrayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var element = d as Image;

            if ((bool)e.NewValue)
            {
                SetGrayscale(element);
            }
            else
            {
                SetDefalut(element);
            }
        }

        private static void SetGrayscale(System.Windows.Controls.Image img)
        {
            img.IsEnabled = false;

            FormatConvertedBitmap bitmap = new FormatConvertedBitmap();
            bitmap.BeginInit();
            bitmap.Source = (BitmapSource)img.Source;
            bitmap.DestinationFormat = PixelFormats.Gray32Float;
            bitmap.EndInit();

            img.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() => { img.Source = bitmap; }));
        }

        private static void SetDefalut(System.Windows.Controls.Image img)
        {
            img.IsEnabled = true;

            BitmapImage tempImg = new BitmapImage();
            tempImg.BeginInit();
            tempImg.UriSource = new Uri("pack://application:,,,/TestForWpf;component/Images/icon.png");
            tempImg.EndInit();
            img.Source = tempImg;
        }
    }
}
