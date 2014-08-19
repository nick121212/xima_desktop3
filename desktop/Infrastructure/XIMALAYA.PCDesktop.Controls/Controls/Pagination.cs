using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;

namespace XIMALAYA.PCDesktop.Controls
{
    /// <summary>
    /// 当前页码修改事件参数
    /// </summary>
    public class CurrentPageChangedArgs : RoutedEventArgs
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="routedEvent"></param>
        /// <param name="source"></param>
        public CurrentPageChangedArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source) { }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 当前页对应的数量
        /// </summary>
        public int PageSize { get; set; }
    }
    /// <summary>
    /// 当前页码修改事件委托
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void CurrentPageChangedHandler(object sender, CurrentPageChangedArgs e);
    /// <summary>
    /// 分页控件
    /// </summary>
    [TemplatePart(Name = Pagination.PART_Pages, Type = typeof(StackPanel))]
    [TemplatePart(Name = Pagination.PART_PrevBtn, Type = typeof(MyToggleButton))]
    [TemplatePart(Name = Pagination.PART_NextBtn, Type = typeof(MyToggleButton))]
    public class Pagination : Control
    {
        #region 常量

        private const string PART_Pages = "PART_Pages";
        private const string PART_PrevBtn = "PART_PrevBtn";
        private const string PART_NextBtn = "PART_NextBtn";

        #endregion

        #region 依赖项属性

        /// <summary>
        /// 当前页码更改事件
        /// </summary>
        public event CurrentPageChangedHandler CurrnetPageChanedRoute
        {
            add { AddHandler(CurrnetPageChanedRouteEvent, value); }
            remove { RemoveHandler(CurrnetPageChanedRouteEvent, value); }
        }
        /// <summary>
        /// 路由属性，当前page修改时触发
        /// </summary>
        public static readonly RoutedEvent CurrnetPageChanedRouteEvent =
            EventManager.RegisterRoutedEvent("CurrnetPageChanedRoute", RoutingStrategy.Bubble, typeof(CurrentPageChangedHandler), typeof(Pagination));
        /// <summary>
        /// 是否显示上一页和下一页按钮，这是个依赖属性
        /// </summary>
        public Visibility IsShowNextPrevBtn
        {
            get { return (Visibility)GetValue(IsShowNextPrevBtnProperty); }
            set { SetValue(IsShowNextPrevBtnProperty, value); }
        }
        /// <summary>
        /// 是否显示上一页和下一页按钮，这是个依赖属性
        /// </summary>
        public static readonly DependencyProperty IsShowNextPrevBtnProperty =
            DependencyProperty.Register("IsShowNextPrevBtn", typeof(Visibility), typeof(Pagination), new PropertyMetadata(Visibility.Hidden));
        /// <summary>
        /// 总数量，这是个依赖属性
        /// </summary>
        public long Total
        {
            get { return (long)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }
        /// <summary>
        /// 总数量，这是个依赖属性
        /// </summary>
        public static readonly DependencyProperty TotalProperty = DependencyProperty.Register("Total", typeof(long), typeof(Pagination), new PropertyMetadata(0L, OnTotalChanged));
        /// <summary>
        /// 每页的数量，这是个依赖属性
        /// </summary>
        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }
        /// <summary>
        /// 每页的数量，这是个依赖属性
        /// </summary>
        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(Pagination), new PropertyMetadata(30, OnPageSizeChanged));
        /// <summary>
        /// 当前页码，这是个依赖属性
        /// </summary>
        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set
            {
                SetValue(CurrentPageProperty, value);
                this.ChangePageCommand.RaiseCanExecuteChanged();
                this.NextPageCommand.RaiseCanExecuteChanged();
                this.PrevPageCommand.RaiseCanExecuteChanged();
                if (this.CurrentPage > 0 && this.CurrentPage <= this.TotalPage)
                {
                    //CurrnetPageChanedTouteEvent

                    this.RaiseEvent(new CurrentPageChangedArgs(CurrnetPageChanedRouteEvent, this)
                    {
                        CurrentPage = this.CurrentPage,
                        PageSize = this.PageSize
                    });

                    //this.dog.Invoker(new RunRoutedEventArgs(Dog.RunRoutedEvent, this) { Name = "Wangwang" });  
                }
            }
        }
        /// <summary>
        /// 当前页码，这是个依赖属性
        /// </summary>
        public static readonly DependencyProperty CurrentPageProperty = DependencyProperty.Register("CurrentPage", typeof(int), typeof(Pagination), new PropertyMetadata(1, OnCurrentPageChanged));
        /// <summary>
        /// 分页内部用多少个元素，这是个依赖属性
        /// </summary>
        public int InnerCount
        {
            get { return (int)GetValue(InnerCountProperty); }
            set { SetValue(InnerCountProperty, value); }
        }
        /// <summary>
        /// 分页内部用多少个元素，这是个依赖属性
        /// </summary>
        public static readonly DependencyProperty InnerCountProperty = DependencyProperty.Register("InnerCount", typeof(int), typeof(Pagination), new PropertyMetadata(4, OnCurrentPageChanged));
        /// <summary>
        /// 分页外部用多少个元素，这是个依赖属性
        /// </summary>
        public int OuterCount
        {
            get { return (int)GetValue(OuterCountProperty); }
            set { SetValue(OuterCountProperty, value); }
        }
        /// <summary>
        /// 分页外部用多少个元素，这是个依赖属性
        /// </summary>
        public static readonly DependencyProperty OuterCountProperty = DependencyProperty.Register("OuterCount", typeof(int), typeof(Pagination), new PropertyMetadata(1, OnCurrentPageChanged));
        /// <summary>
        /// 按钮的样式
        /// </summary>
        public Style ButtonStyle
        {
            get { return (Style)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }
        /// <summary>
        /// 按钮的样式
        /// </summary>
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register("ButtonStyle", typeof(Style), typeof(Pagination), new PropertyMetadata(null));

        #endregion

        #region 命令

        /// <summary>
        /// 更改页码命令
        /// </summary>
        public DelegateCommand<int?> ChangePageCommand { get; set; }
        /// <summary>
        /// 更改页码命令
        /// </summary>
        public DelegateCommand NextPageCommand { get; set; }
        /// <summary>
        /// 更改页码命令
        /// </summary>
        public DelegateCommand PrevPageCommand { get; set; }

        #endregion

        #region 属性

        /// <summary>
        /// 存放页码按钮的容器
        /// </summary>
        private StackPanel StackPanel { get; set; }
        /// <summary>
        /// 下一页按钮
        /// </summary>
        private MyToggleButton NextBtn { get; set; }
        /// <summary>
        /// 上一页按钮
        /// </summary>
        private MyToggleButton PrevBtn { get; set; }
        /// <summary>
        /// 根据Total和PageSize计算总共多少页
        /// </summary>
        private int TotalPage { get; set; }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public Pagination()
        {
            this.ChangePageCommand = new DelegateCommand<int?>((pageNo) =>
            {
                if (pageNo.HasValue && pageNo > 0)
                {
                    this.CurrentPage = (int)pageNo;
                }
            }, (pageNo) =>
            {
                if (pageNo.HasValue)
                {
                    return (int)pageNo != this.CurrentPage;
                }
                return true;
            });

            this.PrevPageCommand = new DelegateCommand(() =>
            {
                this.CurrentPage -= 1;
            }, () =>
            {
                return this.CurrentPage > 1;
            });
            this.NextPageCommand = new DelegateCommand(() =>
            {
                this.CurrentPage += 1;
            }, () =>
            {
                return this.CurrentPage != this.TotalPage;
            });
        }

        #endregion

        #region Control 虚拟方法

        /// <summary>
        /// 应用模板
        /// </summary>
        public override void OnApplyTemplate()
        {

            base.OnApplyTemplate();

            this.StackPanel = GetTemplateChild(Pagination.PART_Pages) as StackPanel;
            this.NextBtn = GetTemplateChild(Pagination.PART_NextBtn) as MyToggleButton;
            this.PrevBtn = GetTemplateChild(Pagination.PART_PrevBtn) as MyToggleButton;

            this.NextBtn.Command = this.NextPageCommand;
            this.PrevBtn.Command = this.PrevPageCommand;

            this.CalcTotalPage();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 计算总页码数
        /// </summary>
        private void CalcTotalPage()
        {
            if (this.PageSize <= 0)
            {
                throw new ArgumentException("PageSize只能是大于0的整数！");
            }

            this.TotalPage = (int)Math.Ceiling((double)this.Total / this.PageSize);
            if (this.TotalPage == 1)
            {
                this.IsShowNextPrevBtn = System.Windows.Visibility.Collapsed;
            }
            else
            {
                this.IsShowNextPrevBtn = System.Windows.Visibility.Visible;
            }
            this.ChangePages();
        }
        /// <summary>
        /// 改变当前页码组合
        /// </summary>
        internal void ChangePages()
        {
            if (this.StackPanel == null) return;
            this.StackPanel.Children.Clear();
            if (this.Total <= 0)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
                return;
            }
            if (this.CurrentPage <= 0)
            {
                this.CurrentPage = 1;
                return;
            }
            if (this.CurrentPage > this.TotalPage)
            {
                this.CurrentPage = this.TotalPage;
                return;
            }
            if (this.TotalPage < 2)
            {
                this.Visibility = System.Windows.Visibility.Hidden;
                return;
            }
            this.Visibility = System.Windows.Visibility.Visible;
            if (this.CurrentPage < this.InnerCount * 2)
            {
                this.AddPageNoFront();
                return;
            }

            if (this.CurrentPage > this.TotalPage - this.InnerCount * 2 + this.OuterCount)
            {
                this.AddPageNoBack();
                return;
            }

            this.AddPageNoNormal();
        }

        private void AddPageNoBack()
        {
            int min = this.CurrentPage - this.InnerCount + 1;
            int max = this.CurrentPage + this.InnerCount - 1;// this.TotalPage + 1;

            if (max > this.TotalPage)
            {
                min -= max - this.TotalPage;
            }
            max = this.TotalPage + 1;

            if (min > this.OuterCount + 2)
            {
                this.AddFrontOuter();
                this.AddSplit();
            }
            else
            {
                min = 1;
            }

            for (; min < max; min++)
            {
                this.AddPageNo(min);
            }
        }

        private void AddPageNoNormal()
        {
            int min = this.CurrentPage - this.InnerCount + 1;
            int max = this.CurrentPage + this.InnerCount;

            this.AddFrontOuter();
            this.AddSplit();
            for (; min < max; min++)
            {
                this.AddPageNo(min);
            }
            this.AddSplit();
            this.AddBackOuter();
        }

        private void AddPageNoFront()
        {
            int min = this.CurrentPage - this.InnerCount;
            int max = this.CurrentPage + this.InnerCount;

            if (min < 0)
            {
                max -= min;
            }
            min = 1;
            for (; min < max; min++)
            {
                if (min > this.TotalPage) return;
                this.AddPageNo(min);
            }

            if (this.TotalPage - max > this.OuterCount)
            {
                this.AddSplit();
                this.AddBackOuter();
            }
            else
            {
                for (; max <= this.TotalPage; max++)
                {
                    this.AddPageNo(max);
                }
            }

            return;
        }

        private void AddFrontOuter()
        {
            int max = this.OuterCount + 1 > this.TotalPage ? this.TotalPage : this.OuterCount + 1;

            for (int i = 0; i < max; i++)
            {
                this.AddPageNo(i + 1);
            }
        }
        private void AddBackOuter()
        {
            int max = this.TotalPage - this.OuterCount;

            for (int i = max; i <= this.TotalPage; i++)
            {
                this.AddPageNo(i);
            }
        }
        private void AddSplit()
        {
            this.StackPanel.Children.Add(new Label
            {
                Content = "......",
                Margin = this.Margin
            });
        }
        private void AddPageNo(int pageNo)
        {
            var toggleButton = new MyToggleButton
            {
                Content = pageNo,
                ContentChecked = pageNo,
                Command = this.ChangePageCommand,
                CommandParameter = pageNo,
                IsBackground = System.Windows.Visibility.Visible,
                IsChecked = pageNo == this.CurrentPage,
                IsEnabled = pageNo != this.CurrentPage,
                Padding = new Thickness(),
                Style = this.ButtonStyle
            };
            this.StackPanel.Children.Add(toggleButton);
        }
        /// <summary>
        /// Total变量改变回调
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnTotalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = d as Pagination;

            pagination.CalcTotalPage();
        }
        /// <summary>
        /// PageSize变量改变回调
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnPageSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = d as Pagination;

            pagination.CalcTotalPage();
        }
        /// <summary>
        /// 当前页码更改回调
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnCurrentPageChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pagination = d as Pagination;

            pagination.ChangePages();
        }

        #endregion
    }
}
