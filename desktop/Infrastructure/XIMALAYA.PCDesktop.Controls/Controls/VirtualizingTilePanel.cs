using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
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
    /// 虚拟化容器
    /// </summary>
    public class VirtualizingTilePanel : VirtualizingPanel, IScrollInfo
    {
        #region 字段

        private bool _canHorizontallyScroll;
        private bool _canVerticallyScroll;
        private TranslateTransform _trans = new TranslateTransform();

        #endregion

        #region 依赖属性

        /// <summary>
        /// 控件的宽度
        /// </summary>
        public double ItemWidth
        {
            get { return (double)GetValue(ItemWidthProperty); }
            set { SetValue(ItemWidthProperty, value); }
        }
        /// <summary>
        /// 控件的宽度
        /// </summary>
        public static readonly DependencyProperty ItemWidthProperty =
            DependencyProperty.Register("ItemWidth", typeof(double), typeof(VirtualizingTilePanel), new PropertyMetadata(100D));
        /// <summary>
        /// 控件的高度
        /// </summary>
        public double ItemHeight
        {
            get { return (double)GetValue(ItemHeightProperty); }
            set { SetValue(ItemHeightProperty, value); }
        }
        /// <summary>
        /// 控件的高度
        /// </summary>
        public static readonly DependencyProperty ItemHeightProperty =
            DependencyProperty.Register("ItemHeight", typeof(double), typeof(VirtualizingTilePanel), new PropertyMetadata(100D));

        #endregion

        #region 属性

        /// <summary>
        /// 滚动条偏移
        /// </summary>
        private Point Offset = new Point();
        /// <summary>
        /// 可视区域大小
        /// </summary>
        private Size ViewportSize = new Size();
        /// <summary>
        /// 测量出来的尺寸
        /// </summary>
        private Size ActualSize = new Size();
        /// <summary>
        /// 可以容下多少列
        /// </summary>
        private int HorizontalChildMaxCount { get; set; }
        /// <summary>
        /// 可以容下多少行
        /// </summary>
        private int VerticalChildMaxCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private Double MinOffset { get; set; }

        #endregion

        #region IScrollInfo 成员

        /// <summary>
        /// 获取或设置一个值，该值指示能否在水平轴上滚动。
        /// </summary>
        public bool CanHorizontallyScroll
        {
            get { return _canHorizontallyScroll; }
            set { _canHorizontallyScroll = value; }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示能否在垂直轴上滚动
        /// </summary>
        public bool CanVerticallyScroll
        {
            get { return _canVerticallyScroll; }
            set { _canVerticallyScroll = value; }
        }
        /// <summary>
        /// 获取范围的垂直大小。
        /// </summary>
        public double ExtentHeight
        {
            get { return this.ActualSize.Height; }
        }
        /// <summary>
        /// 获取范围的水平大小。
        /// </summary>
        public double ExtentWidth
        {
            get { return this.ActualSize.Width; }
        }
        /// <summary>
        /// 获取滚动内容的水平偏移
        /// </summary>
        public double HorizontalOffset
        {
            get { return this.Offset.X; }
        }
        /// <summary>
        /// 获取滚动内容的垂直偏移。
        /// </summary>
        public double VerticalOffset
        {
            get { return this.Offset.Y; }
        }
        /// <summary>
        /// 获取此内容的视区的垂直大小。
        /// </summary>
        public double ViewportHeight
        {
            get { return this.ViewportSize.Height; }
        }
        /// <summary>
        /// 获取此内容的视区的水平大小。
        /// </summary>
        public double ViewportWidth
        {
            get { return this.ViewportSize.Width; }
        }
        /// <summary>
        /// 获取或设置一个控制滚动行为的 ScrollViewer 元素。
        /// </summary>
        public ScrollViewer ScrollOwner { get; set; }
        /// <summary>
        /// 在内容内向下滚动一个逻辑单元。
        /// </summary>
        public void LineDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.MinOffset);
        }
        /// <summary>
        /// 在内容内向上滚动一个逻辑单元。
        /// </summary>
        public void LineUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.MinOffset);
        }
        /// <summary>
        /// 
        /// </summary>
        public void LineLeft()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public void LineRight()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public Rect MakeVisible(Visual visual, Rect rectangle)
        {
            return new Rect();
        }
        /// <summary>
        /// 
        /// </summary>
        public void MouseWheelDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.MinOffset);
        }
        /// <summary>
        /// 
        /// </summary>
        public void MouseWheelLeft()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public void MouseWheelRight()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public void MouseWheelUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.MinOffset);
        }
        /// <summary>
        /// 
        /// </summary>
        public void PageDown()
        {
            this.SetVerticalOffset(this.VerticalOffset + this.MinOffset * 2);
        }
        /// <summary>
        /// 
        /// </summary>
        public void PageLeft()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public void PageRight()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        public void PageUp()
        {
            this.SetVerticalOffset(this.VerticalOffset - this.MinOffset * 2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        public void SetHorizontalOffset(double offset)
        {
            InvalidateMeasure();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="offset"></param>
        public void SetVerticalOffset(double offset)
        {
            if (offset < 0 || this.ViewportSize.Height >= this.ActualSize.Height)
            {
                offset = 0;
            }
            else
            {
                if (offset + this.ViewportSize.Height >= this.ActualSize.Height)
                {
                    offset = this.ActualSize.Height - this.ViewportSize.Height;
                }
            }

            this.Offset.Y = offset;
            if (this.ScrollOwner != null)
                this.ScrollOwner.InvalidateScrollInfo();
            _trans.Y = -offset;
            InvalidateMeasure();
        }

        #endregion

        #region VirtualizingPanel 方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="availableSize"></param>
        /// <returns></returns>
        protected override Size MeasureOverride(Size availableSize)
        {
            int firstIndex, lastIndex;

            if (availableSize.IsEmpty || double.IsInfinity(availableSize.Height) || double.IsInfinity(availableSize.Width))
            {
                return new Size();
                //throw new Exception(string.Format("PagerPanel不支持该尺寸下的布局, availableSize:{0}", availableSize));
            }

            //if (ChildWidth > availableSize.Width || ChildHeight > availableSize.Height)
            //{
            //    throw new Exception(string.Format("子元素尺寸不应大于布局尺寸, availableSize:{0},ChildWidth:{1}, ChildHeight: {2} ", availableSize, ChildWidth, ChildHeight));
            //}


            this.UpdateScrollInfo(availableSize);
            this.DumpGeneratorContent(availableSize);
            this.CalcVisibleItemIndexRange(availableSize, out firstIndex, out lastIndex);
            this.MeasureChild(availableSize, firstIndex, lastIndex);

            return availableSize;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="finalSize"></param>
        /// <returns></returns>
        protected override Size ArrangeOverride(Size finalSize)
        {
            int firstIndex, lastIndex;

            this.UpdateScrollInfo(finalSize);
            this.CalcVisibleItemIndexRange(finalSize, out firstIndex, out lastIndex);

            for (int index = firstIndex; index <= lastIndex; index++)
            {
                if (index < firstIndex || index > lastIndex)
                {
                    continue;
                }

                // 计算子元素坐标
                int x = index % this.HorizontalChildMaxCount;
                int y = (int)Math.Ceiling((double)((index + 0) / this.HorizontalChildMaxCount));

                UIElement child = InternalChildren[index - firstIndex];
                // 将坐标换算成容器中对应布局区域信息
                var rec = new Rect(new Point(x * this.ItemWidth, y * this.ItemHeight), new Size(ItemWidth, ItemHeight));

                child.Arrange(rec);
            }

            return finalSize;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 跟新滚动条
        /// </summary>
        /// <param name="availableSize"></param>
        private void UpdateScrollInfo(Size availableSize)
        {
            // See how many items there are
            ItemsControl itemsControl = ItemsControl.GetItemsOwner(this);
            int itemCount = itemsControl.HasItems ? itemsControl.Items.Count : 0;

            Size extent = this.CalcActualSize(availableSize, itemCount);
            // Update extent
            if (extent != this.ActualSize)
            {
                this.ActualSize = extent;
                if (this.ScrollOwner != null)
                    this.ScrollOwner.InvalidateScrollInfo();
            }

            // Update viewport
            if (availableSize != this.ViewportSize)
            {
                this.ViewportSize = availableSize;
                if (this.ScrollOwner != null)
                    this.ScrollOwner.InvalidateScrollInfo();
            }

            if (this.ViewportHeight > 0)
            {
                this.MinOffset = this.ActualSize.Height / this.ViewportHeight * 20;

                if (this.MinOffset > this.ViewportHeight / 2)
                {
                    this.MinOffset = this.ViewportHeight / 2;
                }
            }
            else
            {
                this.MinOffset = 20;
            }
        }
        /// <summary>
        /// 计算可视区域尺寸
        /// </summary>
        /// <param name="availableSize"></param>
        /// <param name="childrenCount"></param>
        private Size CalcActualSize(Size availableSize, int childrenCount)
        {
            //一行多少个元素
            this.HorizontalChildMaxCount = (int)(availableSize.Width / this.ItemWidth);
            //一列多少个元素
            this.VerticalChildMaxCount = (int)(availableSize.Height / this.ItemHeight);

            return new Size(this.HorizontalChildMaxCount * this.ItemWidth, Math.Ceiling((double)childrenCount / this.HorizontalChildMaxCount) * this.ItemHeight);
        }
        private void CalcVisibleItemIndexRange(Size availableSize, out int firstIndex, out int lastIndex)
        {
            ItemsControl itemsControl = ItemsControl.GetItemsOwner(this);

            firstIndex = lastIndex = 0;
            if (itemsControl != null && itemsControl.Items != null)
            {
                firstIndex = (int)Math.Floor(this.Offset.Y / this.ItemHeight) * this.HorizontalChildMaxCount;
                lastIndex = (int)Math.Ceiling((this.Offset.Y + this.ViewportHeight) / this.ItemHeight) * this.HorizontalChildMaxCount - 1;
            }

            if (lastIndex >= itemsControl.Items.Count)
            {
                lastIndex = itemsControl.Items.Count - 1;
            }
        }
        /// <summary>
        /// 测量子元素布局,生成需要显示的子元素
        /// </summary>
        /// <param name="availableSize">可用布局尺寸</param>
        /// <param name="firstVisibleChildIndex">第一个显示的子元素索引</param>
        /// <param name="lastVisibleChildIndex">最后一个显示的子元素索引</param>
        private void MeasureChild(Size availableSize, int firstVisibleChildIndex, int lastVisibleChildIndex)
        {
            if (firstVisibleChildIndex < 0)
            {
                return;
            }

            // 注意，在第一次使用 ItemContainerGenerator之前要先访问一下InternalChildren, 
            // 否则ItemContainerGenerator为null，是一个Bug
            UIElementCollection children = InternalChildren;
            IItemContainerGenerator generator = ItemContainerGenerator;

            // 获取第一个可视元素位置信息
            GeneratorPosition position = generator.GeneratorPositionFromIndex(firstVisibleChildIndex);
            // 根据元素位置信息计算子元素索引
            int childIndex = position.Offset == 0 ? position.Index : position.Index + 1;

            using (generator.StartAt(position, GeneratorDirection.Forward, true))
            {
                for (int itemIndex = firstVisibleChildIndex; itemIndex <= lastVisibleChildIndex; itemIndex++, childIndex++)
                {
                    bool isNewlyRealized;   // 用以指示新生成的元素是否是新实体化的

                    // 生成下一个子元素
                    var child = (Control)generator.GenerateNext(out isNewlyRealized);
                    if (isNewlyRealized)
                    {
                        if (childIndex >= children.Count)
                        {
                            AddInternalChild(child);
                        }
                        else
                        {
                            InsertInternalChild(childIndex, child);
                        }
                        generator.PrepareItemContainer(child);
                    }

                    // 测算子元素布局
                    child.Measure(availableSize);
                }
            }
            CleanUpItems(firstVisibleChildIndex, lastVisibleChildIndex);
        }
        /// <summary>
        /// 清理不需要显示的子元素
        /// </summary>
        /// <param name="firstVisibleChildIndex">第一个显示的子元素索引</param>
        /// <param name="lastVisibleChildIndex">最后一个显示的子元素索引</param>
        private void CleanUpItems(int firstVisibleChildIndex, int lastVisibleChildIndex)
        {
            UIElementCollection children = this.InternalChildren;
            IItemContainerGenerator generator = this.ItemContainerGenerator;

            // 清除不需要显示的子元素，注意从集合后向前操作，以免造成操作过程中元素索引发生改变
            for (int i = children.Count - 1; i > -1; i--)
            {
                // 通过已显示的子元素的位置信息得出元素索引
                var childGeneratorPos = new GeneratorPosition(i, 0);
                int itemIndex = generator.IndexFromGeneratorPosition(childGeneratorPos);

                // 移除不再显示的元素
                if (itemIndex < firstVisibleChildIndex || itemIndex > lastVisibleChildIndex)
                {
                    generator.Remove(childGeneratorPos, 1);
                    RemoveInternalChildRange(i, 1);
                }
            }
        }
        /// <summary>
        /// 显示数据GeneratorPosition信息
        /// </summary>
        public void DumpGeneratorContent(Size availableSize)
        {
            UIElementCollection children = this.InternalChildren;
            IItemContainerGenerator generator = this.ItemContainerGenerator;
            ItemsControl itemsControl = ItemsControl.GetItemsOwner(this);

            Console.WriteLine("Generator positions:");
            //  this.CalcActualSize(availableSize, itemsControl.Items.Count);
            this.ViewportSize = availableSize;

        }
        /// <summary>
        /// 实例化子元素
        /// </summary>
        /// <param name="itemIndex">数据条目索引</param>
        public void RealizeChild(int itemIndex)
        {
            IItemContainerGenerator generator = this.ItemContainerGenerator;
            GeneratorPosition position = generator.GeneratorPositionFromIndex(itemIndex);

            using (generator.StartAt(position, GeneratorDirection.Forward, allowStartAtRealizedItem: true))
            {
                bool isNewlyRealized;
                // 实例化(构造出空的子元素UI容器)
                var child = (UIElement)generator.GenerateNext(out isNewlyRealized);

                if (isNewlyRealized)
                {
                    // 填充UI容器数据
                    generator.PrepareItemContainer(child);
                }
            }
        }
        /// <summary>
        /// 虚拟化子元素
        /// </summary>
        /// <param name="itemIndex">数据条目索引</param>
        public void VirtualizeChild(int itemIndex)
        {
            IItemContainerGenerator generator = this.ItemContainerGenerator;
            var childGeneratorPos = generator.GeneratorPositionFromIndex(itemIndex);
            if (childGeneratorPos.Offset == 0)
            {
                // 虚拟化(从子元素UI容器中清除数据)
                generator.Remove(childGeneratorPos, 1);
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public VirtualizingTilePanel()
        {
            this.RenderTransform = _trans;
        }

        #endregion
    }
}
