using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace XIMALAYA.PCDesktop.Controls
{
    [TemplatePart(Name = "PART_ContentHost", Type = typeof(ContentPresenter))]
    public class Marquee : ContentControl
    {
        #region Dependency Properties

        #region Loop

        /// <summary>
        /// Gets/Sets The loop of the animation. Zero represents forever
        /// </summary>
        public int Loop
        {
            get { return (int)GetValue(LoopProperty); }
            set { SetValue(LoopProperty, value); }
        }

        public static readonly DependencyProperty LoopProperty = DependencyProperty.Register(
            "Loop",
            typeof(int),
            typeof(Marquee),
            new FrameworkPropertyMetadata(0, OnMarqueeParameterChanged, OnCoerceLoop));

        private static object OnCoerceLoop(DependencyObject sender, object value)
        {
            int loop = (int)value;
            if (loop < 0)
            {
                throw new ArgumentException("Loop必须大于等于0");
            }
            return value;
        }

        #endregion

        #region Direction

        /// <summary>
        /// Gets/Sets the direction of the animation.
        /// </summary>
        public Direction Direction
        {
            get { return (Direction)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction",
            typeof(Direction),
            typeof(Marquee),
            new FrameworkPropertyMetadata(Direction.Right, OnMarqueeParameterChanged));

        #endregion

        #region Behavior

        /// <summary>
        /// Gets/Sets The animation behavior.
        /// </summary>
        public MarqueeBehavior Behavior
        {
            get { return (MarqueeBehavior)GetValue(BehaviorProperty); }
            set { SetValue(BehaviorProperty, value); }
        }

        public static readonly DependencyProperty BehaviorProperty = DependencyProperty.Register(
            "Behavior",
            typeof(MarqueeBehavior),
            typeof(Marquee),
            new FrameworkPropertyMetadata(MarqueeBehavior.Scroll, OnMarqueeParameterChanged));

        #endregion

        #region ScrollAmount

        /// <summary>
        /// Gets/Sets The offset each time the content moves
        /// </summary>
        public double ScrollAmount
        {
            get { return (double)GetValue(ScrollAmountProperty); }
            set { SetValue(ScrollAmountProperty, value); }
        }

        public static readonly DependencyProperty ScrollAmountProperty = DependencyProperty.Register(
            "ScrollAmount",
            typeof(double),
            typeof(Marquee),
            new FrameworkPropertyMetadata(10.0, OnMarqueeParameterChanged, OnCoerceScrollAmount));

        private static object OnCoerceScrollAmount(DependencyObject sender, object value)
        {
            double amount = (double)value;
            if (amount - double.Epsilon <= 0.0)
            {
                throw new ArgumentException("ScrollAmount必须大于0");
            }
            return value;
        }

        #endregion

        #region ScrollDelay

        /// <summary>
        /// Gets/Sets The time delay between each movement.
        /// </summary>
        public double ScrollDelay
        {
            get { return (double)GetValue(ScrollDelayProperty); }
            set { SetValue(ScrollDelayProperty, value); }
        }

        public static readonly DependencyProperty ScrollDelayProperty = DependencyProperty.Register(
            "ScrollDelay",
            typeof(double),
            typeof(Marquee),
            new FrameworkPropertyMetadata(100.0, OnMarqueeParameterChanged, OnCoerceScrollDelay));

        private static object OnCoerceScrollDelay(DependencyObject sender, object value)
        {
            double delay = (double)value;
            if (delay - double.Epsilon <= 0.0)
            {
                throw new ArgumentException("ScrollDelay必须大于0");
            }
            return value;
        }

        #endregion

        private static void OnMarqueeParameterChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Marquee marquee = sender as Marquee;
            if (marquee != null)
            {
                marquee.ResetAnimation();
            }
        }

        #endregion

        #region Private Fields

        private ContentPresenter _contentHost = null;
        private int _currentLoop = -1;
        private DispatcherTimer _timer = null;
        private TranslateTransform _contentTranlate = null;
        // 用于Loop的增量计算
        private int _loopCounter = 1;
        // 当前的Direction
        private Direction _currentDirection;

        #endregion

        #region Initialization

        static Marquee()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Marquee), new FrameworkPropertyMetadata(typeof(Marquee)));
            ClipToBoundsProperty.OverrideMetadata(typeof(Marquee), new FrameworkPropertyMetadata(true));
        }

        public Marquee()
        {
            this.Loaded += new RoutedEventHandler(Marquee_Loaded);
            this.Unloaded += new RoutedEventHandler(Marquee_Unloaded);
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.OnUpdateContentPosition();
        }

        private void Marquee_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Start();
        }

        private void Marquee_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _contentHost = this.GetTemplateChild("PART_ContentHost") as ContentPresenter;
            ResetAnimation();
        }

        #endregion

        #region Logic for Animation

        // When size changed, reset animation
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            this.ResetAnimation();
        }

        // Clear current animation, and restore content's position
        private void ResetAnimation()
        {
            if (_contentHost != null)
            {
                // 无限循环
                if (this.Loop == 0)
                {
                    _currentLoop = -1;
                    _loopCounter = 0;
                }
                else
                {
                    _currentLoop = 0;
                    _loopCounter = 1;
                }

                _timer.Interval = TimeSpan.FromMilliseconds(this.ScrollDelay);
                _currentDirection = this.Direction;

                if (this.Direction == Direction.Left)
                {
                    // 放到最右边
                    _contentTranlate = new TranslateTransform(this.ActualWidth, 0.0);
                }
                else if (this.Direction == Direction.Right)
                {
                    // 放到最左边
                    _contentTranlate = new TranslateTransform();
                }
                _contentHost.RenderTransform = _contentTranlate;
            }
        }

        // Update position of the content
        protected virtual void OnUpdateContentPosition()
        {
            if (_contentHost != null)
            {
                double contentWidth = _contentHost.DesiredSize.Width;
                double leftBound = 0.0, rightBound = 0.0;

                // Alternate不飞出边界
                if (this.Behavior == MarqueeBehavior.Alternate || this.Behavior == MarqueeBehavior.Slide)
                {
                    leftBound = 0;
                    rightBound = this.ActualWidth - contentWidth;
                }
                // Scroll要飞出边界
                else if (this.Behavior == MarqueeBehavior.Scroll)
                {
                    leftBound = -contentWidth;
                    rightBound = this.ActualWidth;
                }

                // 循环次数的计数
                if (_currentLoop < this.Loop)
                {
                    // 计算位移
                    // 这里逻辑比较重复，可以考虑合并
                    if (_currentDirection == Direction.Left)
                    {
                        _contentTranlate.X -= this.ScrollAmount;

                        // 从右往左到头了
                        if (_contentTranlate.X <= leftBound)
                        {
                            if (this.Behavior == MarqueeBehavior.Scroll || this.Behavior == MarqueeBehavior.Slide)
                            {
                                _contentTranlate.X = rightBound;
                            }
                            else if (this.Behavior == MarqueeBehavior.Alternate)
                            {
                                _currentDirection = Direction.Right;
                            }
                            _currentLoop += _loopCounter;
                        }
                    }
                    else if (_currentDirection == Direction.Right)
                    {
                        _contentTranlate.X += this.ScrollAmount;
                        // 从左往右到头了
                        if (_contentTranlate.X >= rightBound)
                        {
                            if (this.Behavior == MarqueeBehavior.Scroll || this.Behavior == MarqueeBehavior.Slide)
                            {
                                _contentTranlate.X = leftBound;

                            }
                            else if (this.Behavior == MarqueeBehavior.Alternate)
                            {
                                _currentDirection = Direction.Left;
                            }
                            _currentLoop += _loopCounter;
                        }
                    }
                }// End of Loop
                else
                {
                    // 保证动画结束后可见
                    if (_contentTranlate.X < 0)
                    {
                        _contentTranlate.X = 0;
                    }
                    if (_contentTranlate.X > this.ActualWidth - contentWidth)
                    {
                        _contentTranlate.X = this.ActualWidth - contentWidth;
                    }
                }
            }
        }

        #endregion
    }

    public enum Direction
    {
        Left,
        Right
    }

    public enum MarqueeBehavior
    {
        Scroll,
        Slide,
        Alternate,
    }
}
