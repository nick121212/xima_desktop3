using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using XIMALAYA.PCDesktop.Tools.Untils;

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
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
        /// <summary>
        /// 图片的路径
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(String), typeof(MyImage), new PropertyMetadata(string.Empty, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var myImage = d as MyImage;

            if (myImage.Source != string.Empty && myImage.Source != null)
            {
                string appStartupPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                string imagePath = Path.Combine(appStartupPath, "images", MD5Until.GetMD5_32(myImage.Source));

                if (File.Exists(imagePath))
                {
                    try
                    {
                        Uri uri = new Uri(imagePath, UriKind.Absolute);
                        if (myImage.Image != null)
                        {
                            myImage.Image.Source = new BitmapImage(uri);
                        }
                    }
                    catch
                    {

                    }

                    return;
                }

                myImage.DownloadImage(myImage.Source.ToString());
            }
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
            var myImage = d as MyImage;
            string path = "pack://application:,,,/XIMALAYA.PCDesktop.Tools;component/Resources/Images/defaults/" + myImage.DefaultSource;

            if (myImage.DefaultSource != string.Empty)
            {
                try
                {
                    Uri uri = new Uri(path, UriKind.RelativeOrAbsolute);

                    ImageBrush ib = new ImageBrush(new BitmapImage(uri));

                    d.SetValue(Control.BackgroundProperty, ib);
                }
                catch
                {

                }

            }
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActive.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(MyImage), new PropertyMetadata(false));

        private Image Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Image = GetTemplateChild("PART_Image") as Image;
            OnSourceChanged(this, new DependencyPropertyChangedEventArgs());
        }

        async void DownloadImage(string url)
        {
            try
            {
                var uri = new Uri(url);

                if (uri.Scheme == "pack")
                {
                    this.Image.Source = new BitmapImage(uri);
                    return;
                }

                var request = WebRequest.Create(uri);

                using (var response = await request.GetResponseAsync())
                {
                    using (var destStream = new MemoryStream())
                    {
                        var responseStream = response.GetResponseStream();
                        var downloadTask = responseStream.CopyToAsync(destStream);

                        await downloadTask;
                        RefreshUI(downloadTask, destStream);
                    }
                }
            }
            catch
            {
                
            }
        }
        async void RefreshUI(Task downloadTask, MemoryStream stream)
        {
            await Task.WhenAny(downloadTask, Task.Delay(1000));

            var data = stream.ToArray();
            var tmpStream = new MemoryStream(data);
            var bmp = new BitmapImage();

            bmp.BeginInit();
            bmp.StreamSource = tmpStream;
            bmp.EndInit();

            if (downloadTask.IsCompleted)
            {
                this.Image.Source = bmp;
                string appStartupPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                if (!Directory.Exists(Path.Combine(appStartupPath, "images")))
                {
                    Directory.CreateDirectory(Path.Combine(appStartupPath, "images"));
                }

                FileStream dumpFile = new FileStream(Path.Combine(appStartupPath, "images", MD5Until.GetMD5_32(this.Source)), FileMode.Create, FileAccess.ReadWrite);

                stream.WriteTo(dumpFile);

                dumpFile.Close();
                dumpFile.Dispose();
                dumpFile = null;
            }
        }

    }
}
