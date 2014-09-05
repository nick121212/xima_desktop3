using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 
    /// </summary>
    public enum Position
    {
        Left,
        Right,
        Top,
        Bottom
    }
    /// <summary>
    /// 带箭头的grid
    /// </summary>
    [TemplatePart(Name = PART_Arrow, Type = typeof(Rectangle))]
    public class ArrowPopGrid : Label
    {
        const string PART_Arrow = "PART_Arrow";

        #region 依赖项属性

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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ArrowPopGrid), new PropertyMetadata(null));
        /// <summary>
        /// 箭头的尺寸
        /// </summary>
        public double ArrowSize
        {
            get { return (double)GetValue(ArrowSizeProperty); }
            set { SetValue(ArrowSizeProperty, value); }
        }
        /// <summary>
        /// 箭头的尺寸
        /// </summary>
        public static readonly DependencyProperty ArrowSizeProperty =
            DependencyProperty.Register("ArrowSize", typeof(double), typeof(ArrowPopGrid), new PropertyMetadata(20D, OnArrowChange));
        /// <summary>
        /// 箭头的方位
        /// </summary>
        public Position ArrowPosition
        {
            get { return (Position)GetValue(ArrowPositionProperty); }
            set { SetValue(ArrowPositionProperty, value); }
        }
        /// <summary>
        /// 箭头的方位
        /// </summary>
        public static readonly DependencyProperty ArrowPositionProperty =
            DependencyProperty.Register("ArrowPosition", typeof(Position), typeof(ArrowPopGrid), new PropertyMetadata(Position.Top, OnArrowChange));
        /// <summary>
        /// 箭头的位置
        /// </summary>
        public double ArrowPositionSize
        {
            get { return (double)GetValue(ArrowPositionSizeProperty); }
            set { SetValue(ArrowPositionSizeProperty, value); }
        }
        /// <summary>
        /// 箭头的位置
        /// </summary>
        public static readonly DependencyProperty ArrowPositionSizeProperty =
            DependencyProperty.Register("ArrowPositionSize", typeof(double), typeof(ArrowPopGrid), new PropertyMetadata(50D, OnArrowChange));

        private static void OnArrowChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var arrowPopGrid = d as ArrowPopGrid;

            arrowPopGrid.ApplyArrowSize();
        }

        #endregion

        #region 属性

        private Rectangle Rect { get; set; }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.Rect = GetTemplateChild(PART_Arrow) as Rectangle;

            this.ApplyArrowSize();
        }

        private void ApplyArrowSize()
        {
            if (this.Rect == null) return;

            //对角线长度/2
            double halfDiagonal = Math.Sqrt(2 * Math.Pow(this.ArrowSize, 2)) / 2;

            halfDiagonal = Math.Floor(halfDiagonal);
            halfDiagonal -= this.BorderThickness.Top;


            switch (this.ArrowPosition)
            {
                case Position.Left:
                    this.Rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    this.Rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    this.Rect.Margin = new Thickness(-halfDiagonal, this.ArrowPositionSize, 0, 0);
                    break;
                case Position.Right:
                    this.Rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    this.Rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    this.Rect.Margin = new Thickness(0, this.ArrowPositionSize, -halfDiagonal, 0);
                    break;
                case Position.Top:
                    this.Rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    this.Rect.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    this.Rect.Margin = new Thickness(this.ArrowPositionSize, -halfDiagonal, 0, 0);
                    break;
                case Position.Bottom:
                    this.Rect.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    this.Rect.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                    this.Rect.Margin = new Thickness(this.ArrowPositionSize, 0, 0, -halfDiagonal);
                    break;
            }
            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFigure = new PathFigure { IsClosed = true };

            pathGeometry.Figures.Add(pathFigure);

            pathFigure.StartPoint = new Point(0, 0);
            pathFigure.Segments.Add(new LineSegment(new Point(0, this.ArrowSize), false));
            pathFigure.Segments.Add(new LineSegment(new Point(this.ArrowSize, 0), false));

            this.Rect.Clip = pathGeometry;

            //<PathFigure StartPoint="0,0" IsClosed="True">
            //            <LineSegment Point="0,100" />
            //            <LineSegment Point="100,0" />
            //        </PathFigure>
            //    </PathGeometry.Figures>
            //</PathGeometry>

        }
    }
}
