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
using System.Windows.Shapes;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 边框类型
    /// </summary>
    public enum BorderType
    {
        /// <summary>
        /// 
        /// </summary>
        None,
        /// <summary>
        /// 
        /// </summary>
        Rectangle,
        /// <summary>
        /// 
        /// </summary>
        Ellipse
    }
    /// <summary>
    /// 带边框的ToggleButton
    /// </summary>
    [TemplatePart(Name = "PART_Border", Type = typeof(Border))]
    [TemplatePart(Name = "PART_Fill", Type = typeof(Rectangle))]
    [TemplatePart(Name = "Part_Scale", Type = typeof(ScaleTransform))]
    [TemplatePart(Name = "Part_ColorAnimation", Type = typeof(ColorAnimation))]
    [TemplatePart(Name = "Part_ColorCheckAnimation", Type = typeof(ColorAnimation))]
    public class MyToggleButton : ToggleButton
    {
        /// <summary>
        /// 边框类型
        /// </summary>
        public BorderType BorderType
        {
            get { return (BorderType)GetValue(BorderTypeProperty); }
            set { SetValue(BorderTypeProperty, value); }
        }
        /// <summary>
        /// 边框类型
        /// </summary>
        public static readonly DependencyProperty BorderTypeProperty =
            DependencyProperty.Register("BorderType", typeof(BorderType), typeof(MyToggleButton), new PropertyMetadata(BorderType.None, OnBorderTypeChanged));
        /// <summary>
        /// 是否自动设置圆角数值
        /// </summary>
        public bool IsAutoCornerRadius
        {
            get { return (bool)GetValue(IsAutoCornerRadiusProperty); }
            set { SetValue(IsAutoCornerRadiusProperty, value); }
        }
        /// <summary>
        /// 是否自动设置圆角数值
        /// </summary>
        public static readonly DependencyProperty IsAutoCornerRadiusProperty =
            DependencyProperty.Register("IsAutoCornerRadius", typeof(bool), typeof(MyToggleButton), new PropertyMetadata(true, OnBorderTypeChanged));
        /// <summary>
        /// 边框圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        /// <summary>
        /// 边框圆角
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(MyToggleButton), new PropertyMetadata(new CornerRadius()));
        /// <summary>
        /// 是否显示边框
        /// </summary>
        public bool IsShowBorder
        {
            get { return (bool)GetValue(IsShowBorderProperty); }
            set { SetValue(IsShowBorderProperty, value); }
        }
        /// <summary>
        /// 是否显示边框
        /// </summary>
        public static readonly DependencyProperty IsShowBorderProperty =
            DependencyProperty.Register("IsShowBorder", typeof(bool), typeof(MyToggleButton), new PropertyMetadata(false));
        /// <summary>
        /// 矩形的圆角
        /// </summary>
        public double RadiusSize
        {
            get { return (double)GetValue(RadiusSizeProperty); }
            set { SetValue(RadiusSizeProperty, value); }
        }
        /// <summary>
        /// 矩形的圆角
        /// </summary>
        public static readonly DependencyProperty RadiusSizeProperty =
            DependencyProperty.Register("RadiusSize", typeof(double), typeof(MyToggleButton), new PropertyMetadata(0D));
        /// <summary>
        /// 选中后的背景色
        /// </summary>
        public Brush BackgroundChecked
        {
            get { return (Brush)GetValue(BackgroundCheckedProperty); }
            set { SetValue(BackgroundCheckedProperty, value); }
        }
        /// <summary>
        /// 选中后的背景色
        /// </summary>
        public static readonly DependencyProperty BackgroundCheckedProperty =
            DependencyProperty.Register("BackgroundChecked", typeof(Brush), typeof(MyToggleButton), new PropertyMetadata(new SolidColorBrush()));
        /// <summary>
        /// 选中后的前景色
        /// </summary>
        public Brush ForegroundChecked
        {
            get { return (Brush)GetValue(ForegroundCheckedProperty); }
            set { SetValue(ForegroundCheckedProperty, value); }
        }
        /// <summary>
        /// 选中后的前景色
        /// </summary>
        public static readonly DependencyProperty ForegroundCheckedProperty =
            DependencyProperty.Register("ForegroundChecked", typeof(Brush), typeof(MyToggleButton), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));
        /// <summary>
        /// 选中后的内容
        /// </summary>
        public object ContentChecked
        {
            get { return (object)GetValue(ContentCheckedProperty); }
            set { SetValue(ContentCheckedProperty, value); }
        }
        /// <summary>
        /// 选中后的内容
        /// </summary>
        public static readonly DependencyProperty ContentCheckedProperty =
            DependencyProperty.Register("ContentChecked", typeof(object), typeof(MyToggleButton), new PropertyMetadata(null));
        /// <summary>
        /// 第三状态下的内容
        /// </summary>
        public object ContentThreeState
        {
            get { return (object)GetValue(ContentThreeStateProperty); }
            set { SetValue(ContentThreeStateProperty, value); }
        }
        /// <summary>
        /// 第三状态下的内容
        /// </summary>
        public static readonly DependencyProperty ContentThreeStateProperty =
            DependencyProperty.Register("ContentThreeState", typeof(object), typeof(MyToggleButton), new PropertyMetadata(null));
        /// <summary>
        /// 背景色控制选项
        /// </summary>
        public Visibility IsBackground
        {
            get { return (Visibility)GetValue(IsBackgroundProperty); }
            set { SetValue(IsBackgroundProperty, value); }
        }
        /// <summary>
        /// 背景色控制选项
        /// </summary>
        public static readonly DependencyProperty IsBackgroundProperty =
            DependencyProperty.Register("IsBackground", typeof(Visibility), typeof(MyToggleButton), new PropertyMetadata(Visibility.Hidden));
        /// <summary>
        /// 是否为单选
        /// </summary>
        public bool IsSingle
        {
            get { return (bool)GetValue(IsSingleProperty); }
            set { SetValue(IsSingleProperty, value); }
        }
        /// <summary>
        /// 是否为单选
        /// </summary>
        public static readonly DependencyProperty IsSingleProperty =
            DependencyProperty.Register("IsSingle", typeof(bool), typeof(MyToggleButton), new PropertyMetadata(false));

        private static void OnBorderTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var myToggleButton = d as MyToggleButton;

            switch (myToggleButton.BorderType)
            {
                case BorderType.None:
                    myToggleButton.IsShowBorder = false;
                    break;
                case BorderType.Ellipse:
                    myToggleButton.IsShowBorder = true;
                    if (myToggleButton.IsAutoCornerRadius)
                    {
                        myToggleButton.CornerRadius = new CornerRadius(myToggleButton.Width / 2);
                        myToggleButton.RadiusSize = myToggleButton.Width / 2;
                    }
                    break;
                case BorderType.Rectangle:
                    myToggleButton.IsShowBorder = true;
                    myToggleButton.CornerRadius = new CornerRadius();
                    myToggleButton.RadiusSize = 0;
                    break;
            }
        }

        private Border Border { get; set; }
        private Rectangle Rectangle { get; set; }
        private ScaleTransform ScaleTransform { get; set; }
        private ColorAnimation ColorAnimation { get; set; }
        private ColorAnimation ColorCheckedAnimation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnToggle()
        {
            if (!this.IsSingle)
            {
                base.OnToggle();
            }
        }
        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Border = GetTemplateChild("PART_Border") as Border;
            this.Rectangle = GetTemplateChild("PART_Fill") as Rectangle;
            this.ScaleTransform = GetTemplateChild("Part_Scale") as ScaleTransform;
            this.ColorAnimation = GetTemplateChild("Part_ColorAnimation") as ColorAnimation;
            this.ColorCheckedAnimation = GetTemplateChild("Part_ColorCheckAnimation") as ColorAnimation;

            if (this.ForegroundChecked == null || this.ForegroundChecked.GetType() != typeof(SolidColorBrush) || ((SolidColorBrush)this.ForegroundChecked).Color == null)
            {
                return;
            }
            if (this.ColorAnimation != null)
            {
                this.ColorAnimation.To = ((SolidColorBrush)this.ForegroundChecked).Color;
            }
            if (this.ColorCheckedAnimation != null)
            {
                this.ColorCheckedAnimation.To = ((SolidColorBrush)this.ForegroundChecked).Color;
            }
        }
    }
}
