﻿using System;
using System.ComponentModel.Composition;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Converter;
using XIMALAYA.PCDesktop.Tools.Setting;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class Shell : IFlyoutFactory
    {
        #region 属性

        /// <summary>
        /// MEF服务
        /// </summary>
        [Import]
        protected IServiceLocator Container { get; set; }
        /// <summary>
        /// 容器管理器
        /// </summary>
        [Import(AllowRecomposition = false)]
        private IRegionManager regionManager { get; set; }
        /// <summary>
        /// viewmodel
        /// </summary>
        [Import]
        public MainViewModel ViewModel
        {
            get { return this.DataContext as MainViewModel; }
            set { this.DataContext = value; }
        }
        /// <summary>
        /// flyout的索引号
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 当前的flyout
        /// </summary>
        private Flyout CurrentFlyout { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private FlyoutsControl ContainerGrid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        private string CurrentRegionName { get; set; }

        #endregion

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        public Shell()
        {
            InitializeComponent();

            this.Loaded += Shell_Loaded;
            this.Closing += Shell_Closing;
        }

        #endregion

        #region 事件

        void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            this.Container.GetInstance(typeof(XMSetting));
            this.ContainerGrid = this.Flyouts;
            RegionManager.SetRegionManager(this.settingFlyout, this.regionManager);

            this.ViewModel.Init(NotifyIcon, this);
            this.StateChanged += Shell_StateChanged;
            this.SetFlyout(string.Empty);
        }

        void Shell_StateChanged(object sender, EventArgs e)
        {
            GlobalDataSingleton.Instance.IsWindowShow = this.WindowState != System.Windows.WindowState.Minimized;
            if (GlobalDataSingleton.Instance.IsWindowShow)
            {
                this.Show();
                this.Activate();
            }
            else
            {
                //this.Hide();
            }
        }

        async void Shell_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!GlobalDataSingleton.Instance.IsActualExit && !GlobalDataSingleton.Instance.IsExit)
            {
                e.Cancel = true;
                this.WindowState = System.Windows.WindowState.Minimized;
                this.Hide();
                if (!GlobalDataSingleton.Instance.IsTrip)
                {
                    GlobalDataSingleton.Instance.IsTrip = true;
                    this.NotifyIcon.ShowBalloonTip(this.ViewModel.WindowTitle, "程序在后台已运行！", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
                }
                return;
            }

            if (GlobalDataSingleton.Instance.IsExitConfirm)
            {
                e.Cancel = GlobalDataSingleton.Instance.IsExitConfirm;
                var mySettings = new MetroDialogSettings()
                {
                    AffirmativeButtonText = "退出",
                    NegativeButtonText = "取消",
                    AnimateShow = true,
                    AnimateHide = false
                };

                var result = await this.ShowMessageAsync("喜马拉雅?",
                    "确定要退出喜马拉雅?",
                    MessageDialogStyle.AffirmativeAndNegative, mySettings);

                GlobalDataSingleton.Instance.IsExit = false;
                if (result == MessageDialogResult.Affirmative)
                {
                    Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
                    Application.Current.Shutdown();
                }
                return;
            }
        }

        #endregion

        #region IFlyoutFactory 成员

        /// <summary>
        /// 显示层
        /// </summary>
        public void ShowFlyout()
        {
            if (this.CurrentFlyout != null)
            {
                this.CurrentFlyout.IsOpen = true;
            }
        }
        /// <summary>
        /// 设置标题
        /// </summary>
        /// <param name="header"></param>
        private void SetHeader(string header)
        {
            if (header != string.Empty)
            {
                this.CurrentFlyout.Header = header;
            }
            else
            {
                this.CurrentFlyout.HeaderTemplate = null;
                this.CurrentFlyout.CloseCommand = new DelegateCommand<Flyout>((con) =>
                {
                    Flyout flyout = con as Flyout;

                    if (flyout != null)
                    {
                        flyout.IsOpen = false;
                    }
                });
            }
        }
        /// <summary>
        /// 获取flyout
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        private string SetFlyout(string header)
        {
            this.CurrentRegionName = string.Format("ViewRegionName_{0}", ++this.Count);

            this.CurrentFlyout = new Flyout();
            this.CurrentFlyout.IsOpen = false;
            this.CurrentFlyout.AnimateOnPositionChange = true;
            this.CurrentFlyout.Theme = FlyoutTheme.Adapt;
            this.CurrentFlyout.VerticalAlignment = System.Windows.VerticalAlignment.Top;

            this.CurrentFlyout.SetBinding(Flyout.WidthProperty, new Binding { Path = new PropertyPath("ActualWidth"), ElementName = this.Name });
            this.CurrentFlyout.SetBinding(Flyout.HeightProperty, new Binding { Path = new PropertyPath("ActualHeight"), ElementName = this.Name, Converter = new ActualSizeFixedConverter(), ConverterParameter = "-,55" });
            RegionManager.SetRegionManager(this.CurrentFlyout, this.regionManager);
            RegionManager.SetRegionName(this.CurrentFlyout, this.CurrentRegionName);
            this.ContainerGrid.Items.Add(this.CurrentFlyout);
            this.CurrentFlyout.ApplyTemplate();
            this.CurrentFlyout.IsVisibleChanged += CurrentFlyout_IsVisibleChanged;
            this.CurrentFlyout.Position = Position.Right;

            return this.CurrentRegionName;
        }
        /// <summary>
        /// flyout显示事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentFlyout_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Flyout flyout = sender as Flyout;

            if (flyout.Visibility == System.Windows.Visibility.Hidden)
            {
                string regionName = string.Empty;

                regionName = RegionManager.GetRegionName(flyout);
                this.regionManager.Regions.Remove(regionName);

                flyout.Content = null;
                flyout.DataContext = null;
                this.ContainerGrid.Items.Remove(flyout);
                flyout = null;
                GC.Collect();
            }
        }
        /// <summary>
        /// 新建层
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public string GetFlyout(string header)
        {
            string regionName = this.CurrentRegionName;

            this.SetHeader(header);
            this.CurrentFlyout.Position = Position.Right;
            this.CurrentFlyout.IsOpen = true;

            this.SetFlyout(string.Empty);

            return regionName;
        }
        /// <summary>
        /// 新建面板
        /// </summary>
        /// <param name="header"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public string GetFlyout(string header, double? Width, double? Height)
        {
            return this.GetFlyout(header, Width, Height, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="isModal"></param>
        /// <returns></returns>
        public string GetFlyout(string header, double? Width, double? Height, bool isModal = false)
        {
            string regionName = this.CurrentRegionName;

            if (Width.HasValue)
            {
                this.CurrentFlyout.Width = (double)Width;
            }
            if (Height.HasValue)
            {
                this.CurrentFlyout.Height = (double)Height;
            }
            this.SetHeader(header);
            this.CurrentFlyout.IsModal = isModal;
            this.CurrentFlyout.Position = Position.Right;
            this.CurrentFlyout.IsOpen = true;

            this.SetFlyout(header);

            return regionName;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="Width"></param>
        /// <param name="Height"></param>
        /// <param name="isModal"></param>
        /// <returns></returns>
        public string GetFlyout(string header, double? Width, double? Height, BaseViewModel veiwModel, Expression<Func<string>> propertyExpression)
        {
            string regionName = this.CurrentRegionName; 
            Binding binding = null;
            var body = propertyExpression.Body as MemberExpression;

            if (Width.HasValue)
            {
                this.CurrentFlyout.Width = (double)Width;
            }
            if (Height.HasValue)
            {
                this.CurrentFlyout.Height = (double)Height;
            }

            this.CurrentFlyout.Position = Position.Right;
            this.CurrentFlyout.IsOpen = true;
            binding = new Binding(body.Member.Name);
            binding.Source = veiwModel;
            this.CurrentFlyout.SetBinding(Flyout.HeaderProperty, binding);

            this.SetFlyout(header);

            return regionName;
        }

        #endregion
    }
}
