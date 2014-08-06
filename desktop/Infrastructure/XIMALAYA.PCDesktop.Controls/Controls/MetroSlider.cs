using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 进度条控件
    /// </summary>
    public class MetroSlider : Slider
    {
        /// <summary>
        /// 滑块是否相反
        /// </summary>
        public bool IsReverse
        {
            get { return (bool)GetValue(IsReverseProperty); }
            set { SetValue(IsReverseProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsReverseProperty =
            DependencyProperty.Register("IsReverse", typeof(bool), typeof(MetroSlider), new PropertyMetadata(false));

        /// <summary>
        /// ThumbForeground
        /// </summary>
        public Brush ThumbForeground
        {
            get { return (Brush)GetValue(ThumbForegroundProperty); }
            set { SetValue(ThumbForegroundProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ThumbForegroundProperty =
            DependencyProperty.Register("ThumbForeground", typeof(Brush), typeof(MetroSlider), new PropertyMetadata(null));

        /// <summary>
        /// ThumbBackground
        /// </summary>
        public Brush ThumbBackground
        {
            get { return (Brush)GetValue(ThumbBackgroundProperty); }
            set { SetValue(ThumbBackgroundProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ThumbBackgroundProperty =
            DependencyProperty.Register("ThumbBackground", typeof(Brush), typeof(MetroSlider), new PropertyMetadata(null));

        /// <summary>
        /// TrackBackgroundFront
        /// </summary>
        public Brush TrackBackgroundFront
        {
            get { return (Brush)GetValue(TrackBackgroundFrontProperty); }
            set { SetValue(TrackBackgroundFrontProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TrackBackgroundFrontProperty =
            DependencyProperty.Register("TrackBackgroundFront", typeof(Brush), typeof(MetroSlider), new PropertyMetadata(null));


        /// <summary>
        /// 
        /// </summary>
        public Brush TrackBackgroundBack
        {
            get { return (Brush)GetValue(TrackBackgroundBackProperty); }
            set { SetValue(TrackBackgroundBackProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TrackBackgroundBackProperty =
            DependencyProperty.Register("TrackBackgroundBack", typeof(Brush), typeof(MetroSlider), new PropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        public double Duration
        {
            get { return (double)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(double), typeof(MetroSlider), new PropertyMetadata(0D));
        /// <summary>
        /// 
        /// </summary>
        public double TotalTimeSpan
        {
            get { return (double)GetValue(TotalTimeSpanProperty); }
            set { SetValue(TotalTimeSpanProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TotalTimeSpanProperty =
            DependencyProperty.Register("TotalTimeSpan", typeof(double), typeof(MetroSlider), new PropertyMetadata(0D, OnTimeTotalChanged));

        /// <summary>
        /// 
        /// </summary>
        public double TotalBuffTimeSpan
        {
            get { return (double)GetValue(TotalBuffTimeSpanProperty); }
            set { SetValue(TotalBuffTimeSpanProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty TotalBuffTimeSpanProperty =
            DependencyProperty.Register("TotalBuffTimeSpan", typeof(double), typeof(MetroSlider), new PropertyMetadata(0D));

        /// <summary>
        /// 
        /// </summary>
        public float Process
        {
            get { return (float)GetValue(ProcessProperty); }
            set { SetValue(ProcessProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ProcessProperty =
            DependencyProperty.Register("Process", typeof(float), typeof(MetroSlider), new PropertyMetadata(0F, OnProcessChanged));


        /// <summary>
        /// 
        /// </summary>
        public double BuffWidth
        {
            get { return (double)GetValue(BuffWidthProperty); }
            set { SetValue(BuffWidthProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public double CurrentWidth
        {
            get { return (double)GetValue(CurrentWidthProperty); }
            set { SetValue(CurrentWidthProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty CurrentWidthProperty =
            DependencyProperty.Register("CurrentWidth", typeof(double), typeof(MetroSlider), new PropertyMetadata(0D));

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty BuffWidthProperty =
            DependencyProperty.Register("BuffWidth", typeof(double), typeof(MetroSlider), new PropertyMetadata(0D));
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <param name="e"></param>
        public static void OnTimeTotalChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            MetroSlider slider = dependencyObject as MetroSlider;

            if (slider.Duration > 0)
            {
                slider.IsReverse = slider.Value / slider.Duration > 0.5D;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <param name="e"></param>
        public static void OnProcessChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            MetroSlider slider = dependencyObject as MetroSlider;

            slider.BuffWidth = slider.ActualWidth * slider.Process;
        }
        /// <summary>
        /// 
        /// </summary>
        public MetroSlider()
        {
            this.SizeChanged += MetroSlider_SizeChanged;
            this.ValueChanged += MetroSlider_ValueChanged;
        }

        void MetroSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            this.CurrentWidth = this.Value / this.Maximum * (this.ActualWidth - 10);
        }

        void MetroSlider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.BuffWidth = this.ActualWidth * this.Process;

        }
        static MetroSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MetroSlider), new FrameworkPropertyMetadata(typeof(MetroSlider)));
        }
    }
}
