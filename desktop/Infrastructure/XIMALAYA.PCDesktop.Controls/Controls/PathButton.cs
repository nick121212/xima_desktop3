using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public class PathButton : ToggleButton
    {
        /// <summary>
        /// 
        /// </summary>
        public Stretch Stretch
        {
            get { return (System.Windows.Media.Stretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register("Stretch", typeof(System.Windows.Media.Stretch), typeof(PathButton), new PropertyMetadata(null));
        /// <summary>
        /// 
        /// </summary>
        public Brush Fill
        {
            get { return (System.Windows.Media.Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(System.Windows.Media.Brush), typeof(PathButton), new PropertyMetadata(null));
        /// <summary>
        /// 
        /// </summary>
        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconDataProperty =
            DependencyProperty.Register("IconData", typeof(Geometry), typeof(PathButton), new PropertyMetadata(null));
        /// <summary>
        /// 
        /// </summary>
        public Geometry IconDataChecked
        {
            get { return (Geometry)GetValue(IconDataCheckedProperty); }
            set { SetValue(IconDataCheckedProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconDataCheckedProperty =
            DependencyProperty.Register("IconDataChecked", typeof(Geometry), typeof(PathButton), new PropertyMetadata(null));
        /// <summary>
        /// 
        /// </summary>
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(PathButton), new PropertyMetadata(0D));
        /// <summary>
        /// 
        /// </summary>
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(PathButton), new PropertyMetadata(new Thickness(0)));
        /// <summary>
        /// 
        /// </summary>
        public Thickness IconCheckedMargin
        {
            get { return (Thickness)GetValue(IconCheckedMarginProperty); }
            set { SetValue(IconCheckedMarginProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconCheckedMarginProperty =
            DependencyProperty.Register("IconCheckedMargin", typeof(Thickness), typeof(PathButton), new PropertyMetadata(new Thickness(0)));
        /// <summary>
        /// 
        /// </summary>
        public bool ShowBackground
        {
            get { return (bool)GetValue(ShowBackgroundProperty); }
            set { SetValue(ShowBackgroundProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ShowBackgroundProperty =
            DependencyProperty.Register("ShowBackground", typeof(bool), typeof(PathButton), new PropertyMetadata(false));
        /// <summary>
        /// 
        /// </summary>
        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(PathButton), new PropertyMetadata(0D));
        /// <summary>
        /// 不支持切换
        /// </summary>
        public bool IsSingle
        {
            get { return (bool)GetValue(IsSingleProperty); }
            set { SetValue(IsSingleProperty, value); }
        }
        /// <summary>
        /// 不支持切换
        /// </summary>
        public static readonly DependencyProperty IsSingleProperty =
            DependencyProperty.Register("IsSingle", typeof(bool), typeof(PathButton), new PropertyMetadata(false));
    
    }
}
