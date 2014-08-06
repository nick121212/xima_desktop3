using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 图片加载器
    /// </summary>
    public static class ImageDecoder
    {
        public static readonly DependencyProperty DefaultSourceProperty;
        public static string GetDefaultSource(FrameworkElement image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("Image");
            }
            return (string)image.GetValue(ImageDecoder.DefaultSourceProperty);
        }
        public static void SetDefaultSource(FrameworkElement image, string value)
        {
            if (image == null)
            {
                throw new ArgumentNullException("Image");
            }
            image.SetValue(ImageDecoder.DefaultSourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty;
        public static string GetSource(Image image)
        {
            if (image == null)
            {
                throw new ArgumentNullException("Image");
            }
            return (string)image.GetValue(ImageDecoder.SourceProperty);
        }
        public static void SetSource(Image image, string value)
        {
            if (image == null)
            {
                throw new ArgumentNullException("Image");
            }
            image.SetValue(ImageDecoder.SourceProperty, value);
        }
        static ImageDecoder()
        {
            ImageDecoder.DefaultSourceProperty = DependencyProperty.RegisterAttached("DefaultSource", typeof(string), typeof(ImageDecoder), new PropertyMetadata(new PropertyChangedCallback(ImageDecoder.InitDefaultSource)));
            ImageDecoder.SourceProperty = DependencyProperty.RegisterAttached("Source", typeof(string), typeof(ImageDecoder), new PropertyMetadata(new PropertyChangedCallback(ImageDecoder.OnSourceWithSourceChanged)));
            ImageQueue.OnComplate += new ImageQueue.ComplateDelegate(ImageDecoder.ImageQueue_OnComplate);
        }

        private static void InitDefaultSource(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageBrush ib = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/XIMALAYA.PCDesktop.Tools;component/Resources/Images/defaults/" + e.NewValue.ToString(), UriKind.RelativeOrAbsolute)));
            d.SetValue(Control.BackgroundProperty, ib);
        }
        private static void ImageQueue_OnComplate(Image i, string u, BitmapImage b)
        {
            //System.Windows.MessageBox.Show(u);
            string source = ImageDecoder.GetSource(i);
            if (source == u.ToString())
            {
                i.Source = b;
                Storyboard storyboard = new Storyboard();
                DoubleAnimation doubleAnimation = new DoubleAnimation(0.0, 1.0, new Duration(TimeSpan.FromMilliseconds(500.0)));
                Storyboard.SetTarget(doubleAnimation, i);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Opacity", new object[0]));
                storyboard.Children.Add(doubleAnimation);
                storyboard.Begin();
            }
        }

        private static BitmapImage GetImage(string url)
        {
            Uri uri = new Uri(url);
            BitmapImage image = null;
            try
            {
                if ("http".Equals(uri.Scheme, StringComparison.CurrentCultureIgnoreCase))
                {
                    WebClient wc = new WebClient();
                    using (var ms = new MemoryStream(wc.DownloadData(uri)))
                    {
                        image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = ms;
                        image.EndInit();
                    }
                }
                else if ("file".Equals(uri.Scheme, StringComparison.CurrentCultureIgnoreCase))
                {
                    using (var fs = new FileStream(url, FileMode.Open))
                    {
                        image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad;
                        image.StreamSource = fs;
                        image.EndInit();
                    }
                }
                else if ("pack".Equals(uri.Scheme, StringComparison.CurrentCultureIgnoreCase))
                {
                    image = new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                }
                if (image != null)
                {
                    if (image.CanFreeze) image.Freeze();

                    return image;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        private static void OnSourceWithSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            //ImageQueue.Queue((Image)o, (string)e.NewValue);

            Image image = o as Image;

            var a = new ImageSourceConverter();

            var imageSouce = (ImageSource)a.ConvertFromString(ImageDecoder.GetSource(image));

            //imageSouce.
        }
    }
}