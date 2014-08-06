using System;
using System.Collections.Generic;
using System.Linq;
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
    /// 图片控件，支持背景默认图
    /// </summary>
    [TemplatePart(Name = "PART_Image", Type = typeof(Image))]
    public class MyImage : Label
    {
        /// <summary>
        /// 图片的路径
        /// </summary>
        public ImageSource Source
        {
            get { return (ImageSource)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        /// <summary>
        /// 图片的路径
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(ImageSource), typeof(MyImage), new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }
        /// <summary>
        /// 图片的默认路径
        /// </summary>
        public string DefaultSource
        {
            get { return (string)GetValue(DefaultSourceProperty); }
            set { SetValue(DefaultSourceProperty, value); }
        }
        /// <summary>
        /// 图片的默认路径
        /// </summary>
        public static readonly DependencyProperty DefaultSourceProperty =
            DependencyProperty.Register("DefaultSource", typeof(string), typeof(MyImage), new PropertyMetadata(string.Empty, OnDefaulImageChanged));

        private static void OnDefaulImageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ImageBrush ib = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/XIMALAYA.PCDesktop.Tools;component/Resources/Images/defaults/" + e.NewValue.ToString(), UriKind.RelativeOrAbsolute)));
            d.SetValue(Control.BackgroundProperty, ib);
        }

        private Image Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Image = GetTemplateChild("PART_Image") as Image;
        }
    }
}
