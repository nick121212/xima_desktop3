using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using XIMALAYA.PCDesktop.Tools;
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
            this.Closed += Shell_Closed;
        }

        #endregion

        #region 事件

        void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            this.Container.GetInstance(typeof(XMSetting));
            RegionManager.SetRegionManager(this.settingFlyout, this.regionManager);
            //定时清理内存
            this.ViewModel.Init(NotifyIcon, this);
            this.StateChanged += Shell_StateChanged;
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

        void Shell_Closed(object sender, EventArgs e)
        {
            this.ViewModel.Dispose();
        }

        async void Shell_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!GlobalDataSingleton.Instance.IsActualExit && !GlobalDataSingleton.Instance.IsExit)
            {
                e.Cancel = true;
                this.WindowState = System.Windows.WindowState.Minimized;
                this.Hide();
                this.NotifyIcon.ShowBalloonTip(this.ViewModel.WindowTitle, "程序在后台已运行！", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
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

        private string SetFlyout(string header)
        {
            string regionName = string.Format("ViewRegionName_{0}", ++this.Count);
            Binding binding = null;
            RelativeSource rs;

            this.CurrentFlyout = new Flyout();
            this.CurrentFlyout.IsOpen = false;
            this.CurrentFlyout.AnimateOnPositionChange = true;
            this.CurrentFlyout.Theme = FlyoutTheme.Adapt;
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

            rs = new RelativeSource(RelativeSourceMode.FindAncestor);
            rs.AncestorType = typeof(Grid);
            binding = new Binding("ActualWidth");
            binding.RelativeSource = rs;
            this.CurrentFlyout.SetBinding(Flyout.WidthProperty, binding);
            RegionManager.SetRegionManager(this.CurrentFlyout, this.regionManager);
            RegionManager.SetRegionName(this.CurrentFlyout, regionName);
            this.ContainerGrid.Items.Add(this.CurrentFlyout);
            this.CurrentFlyout.ApplyTemplate();
            this.CurrentFlyout.IsVisibleChanged += CurrentFlyout_IsVisibleChanged;
            this.CurrentFlyout.Position = Position.Right;

            return regionName;
        }

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
            string regionName = this.GetFlyout(header, null, null);

            this.CurrentFlyout.Position = Position.Right;
            this.CurrentFlyout.IsOpen = true;

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
            string regionName = this.SetFlyout(header);

            if (Width.HasValue)
            {
                this.CurrentFlyout.Width = (double)Width;
            }
            if (Height.HasValue)
            {
                this.CurrentFlyout.Height = (double)Height;
            }

            this.CurrentFlyout.IsModal = isModal;
            this.CurrentFlyout.Position = Position.Right;
            this.CurrentFlyout.IsOpen = true;

            return regionName;
        }

        #endregion
    }
}
