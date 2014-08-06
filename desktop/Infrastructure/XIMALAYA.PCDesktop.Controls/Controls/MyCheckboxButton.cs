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
    /// 带角标的toggle按钮
    /// </summary>
    [TemplatePart(Name = "PART_CheckBorderAnimation", Type = typeof(ThicknessAnimation))]
    [TemplatePart(Name = "PART_BorderAnimation", Type = typeof(ThicknessAnimation))]
    public class MyCheckboxButton : CheckBox
    {
        private ThicknessAnimation BorderAnimation { get; set; }
        private ThicknessAnimation CheckBorderAnimation { get; set; }
        /// <summary>
        /// 角标的尺寸
        /// </summary>
        public double SuperScriptSize
        {
            get { return (double)GetValue(SuperScriptSizeProperty); }
            set { SetValue(SuperScriptSizeProperty, value); }
        }
        /// <summary>
        /// 角标的尺寸
        /// </summary>
        public static readonly DependencyProperty SuperScriptSizeProperty =
            DependencyProperty.Register("SuperScriptSize", typeof(double), typeof(MyCheckboxButton), new PropertyMetadata(10D));
        /// <summary>
        /// 图标的path
        /// </summary>
        public Geometry IconData
        {
            get { return (Geometry)GetValue(IconDataProperty); }
            set { SetValue(IconDataProperty, value); }
        }
        /// <summary>
        /// 图标的path
        /// </summary>
        public static readonly DependencyProperty IconDataProperty =
            DependencyProperty.Register("IconData", typeof(Geometry), typeof(MyCheckboxButton), new PropertyMetadata(null));
        /// <summary>
        /// 图表的宽度
        /// </summary>
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        /// <summary>
        /// 图标的宽度
        /// </summary>
        public static readonly DependencyProperty IconWidthProperty =
            DependencyProperty.Register("IconWidth", typeof(double), typeof(MyCheckboxButton), new PropertyMetadata(15D));
        /// <summary>
        /// 图标的高度
        /// </summary>
        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        /// <summary>
        /// 图标的高度
        /// </summary>
        public static readonly DependencyProperty IconHeightProperty =
            DependencyProperty.Register("IconHeight", typeof(double), typeof(MyCheckboxButton), new PropertyMetadata(15D));
        /// <summary>
        /// 图标margin值
        /// </summary>
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        /// <summary>
        /// 图标margin值
        /// </summary>
        public static readonly DependencyProperty IconMarginProperty =
            DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(MyCheckboxButton), new PropertyMetadata(new Thickness(5)));
        /// <summary>
        /// 图标的填充色
        /// </summary>
        public Brush IconFill
        {
            get { return (Brush)GetValue(IconFillProperty); }
            set { SetValue(IconFillProperty, value); }
        }
        /// <summary>
        /// 图标的填充色
        /// </summary>
        public static readonly DependencyProperty IconFillProperty =
            DependencyProperty.Register("IconFill", typeof(Brush), typeof(MyCheckboxButton), new PropertyMetadata(new SolidColorBrush()));
        /// <summary>
        /// 选中后的边框尺寸
        /// </summary>
        public Thickness CheckedBorderThickness
        {
            get { return (Thickness)GetValue(CheckedBorderThicknessProperty); }
            set { SetValue(CheckedBorderThicknessProperty, value); }
        }
        /// <summary>
        /// 选中后的边框尺寸
        /// </summary>
        public static readonly DependencyProperty CheckedBorderThicknessProperty =
            DependencyProperty.Register("CheckedBorderThickness", typeof(Thickness), typeof(MyCheckboxButton), new PropertyMetadata(new Thickness(1)));

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.BorderAnimation = GetTemplateChild("PART_BorderAnimation") as ThicknessAnimation;
            this.CheckBorderAnimation = GetTemplateChild("PART_CheckBorderAnimation") as ThicknessAnimation;
            this.BorderAnimation.To = this.BorderThickness;
            this.CheckBorderAnimation.To = this.CheckedBorderThickness;
        }

        static MyCheckboxButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyCheckboxButton), new FrameworkPropertyMetadata(typeof(MyCheckboxButton)));
        }
    }
}
