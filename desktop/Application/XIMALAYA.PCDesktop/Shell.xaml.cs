using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Setting;
using XIMALAYA.PCDesktop.Tools.Themes;
using XIMALAYA.PCDesktop.Tools.Untils;

namespace XIMALAYA.PCDesktop
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class Shell : IFlyoutFactory
    {
        #region Dll

        /// <summary>
        /// 释放内存在虚拟内存
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);

        #endregion

        #region 属性

        /// <summary>
        /// 容器管理器
        /// </summary>
        [Import(AllowRecomposition = false)]
        private IRegionManager regionManager { get; set; }
        /// <summary>
        /// viewmodel
        /// </summary>
        [Import]
        public MainViewModel MainViewModel
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
        /// 资源目录
        /// </summary>
        private ResourceDictionary ResourceDic { get; set; }
        /// <summary>
        /// 定时器，用于清理内存
        /// </summary>
        private readonly DispatcherTimer ClearMembryTimer = new DispatcherTimer();

        #endregion

        #region 构造

        /// <summary>
        /// 构造
        /// </summary>
        public Shell()
        {
            InitializeComponent();

            this.Loaded += Shell_Loaded;
            this.Closed += Shell_Closed;
            this.ClearMembryTimer.Interval = TimeSpan.FromSeconds(50);
            this.ClearMembryTimer.Tick += new EventHandler((o, e) =>
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            });
            this.ClearMembryTimer.IsEnabled = true;
        }

        #endregion

        #region 事件

        void Shell_Loaded(object sender, RoutedEventArgs e)
        {
            XMSetting set = XMSetting.Instance;
            this.ResourceDic = ThemeInfoManager.Instance.FindResourceDictionary(@"/MahApps.Metro;component/Styles/Colors.xaml");
            RegionManager.SetRegionManager(this.settingFlyout, this.regionManager);
            this.MainViewModel.NotifyIcon = NotifyIcon;
        }

        void Shell_Closed(object sender, EventArgs e)
        {
            this.ClearMembryTimer.IsEnabled = false;
            this.MainViewModel.Dispose();
        }

        async void Shell_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !this.MainViewModel.IsShutDown;
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

            if (this.MainViewModel.IsShutDown = result == MessageDialogResult.Affirmative)
            {
                Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
                Application.Current.Shutdown();
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

            if (this.ResourceDic != null)
            {
                this.CurrentFlyout.Background = this.ResourceDic["WhiteBrush"] as Brush;
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
