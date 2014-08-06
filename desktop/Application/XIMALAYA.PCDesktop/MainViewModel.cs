using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Tools.Themes;
using XIMALAYA.PCDesktop.Events;
using System.Windows.Forms;
using System.Drawing;

namespace XIMALAYA.PCDesktop
{
    /// <summary>
    /// mvvm model
    /// </summary>
    [Export(typeof(MainViewModel))]
    public class MainViewModel : NotificationObject, IPartImportsSatisfiedNotification, IDisposable
    {
        #region fields

        private string _WindowTitle = string.Empty;

        #endregion

        #region properties

        /// <summary>
        /// 窗体标题
        /// </summary>
        public string WindowTitle
        {
            get
            {
                return _WindowTitle;
            }
            set
            {
                if (value != _WindowTitle)
                {
                    _WindowTitle = value;
                    this.RaisePropertyChanged(() => this.WindowTitle);
                }
            }
        }
        /// <summary>
        /// 系统托盘
        /// </summary>
        private NotifyIcon NotifyIcon { get; set; }
        /// <summary>
        /// 调用模块方法集合
        /// </summary>
        private Dictionary<string, Action> Actions { get; set; }
        /// <summary>
        /// 模块管理器
        /// </summary>
        [Import]
        private IModuleManager ModuleManager { get; set; }
        /// <summary>
        /// 加载的模块集合
        /// </summary>
        [Import]
        private IModuleCatalog ModuleCatalog { get; set; }
        /// <summary>
        /// 事件管理器
        /// </summary>
        [Import]
        private IEventAggregator EventAggregator { get; set; }
        /// <summary>
        /// 是否关闭
        /// </summary>
        public bool IsShutDown { get; set; }

        #endregion

        #region ctor

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="moduleManager">模块管理器</param>
        /// <param name="moduleCatalog">模块目录管理</param>
        /// <param name="eventAggregator">事件管理器</param>
        [ImportingConstructor]
        public MainViewModel(IModuleManager moduleManager,
            IModuleCatalog moduleCatalog,
            IEventAggregator eventAggregator)
        {
            var AccentColors = ThemeInfoManager.Instance.AccentColor;

            this.Actions = new Dictionary<string, Action>();
            this.ModuleManager = moduleManager;
            this.ModuleCatalog = moduleCatalog;
            this.EventAggregator = eventAggregator;

            this.WindowTitle = @"喜马拉雅-听我想听";
            this.InitialTray();

            //订阅加载模块事件
            if (this.EventAggregator != null)
            {
                this.EventAggregator.GetEvent<ModulesManagerEvent>().Subscribe(moduleinfo =>
                {
                    this.LoadModule(moduleinfo.ModuleName, moduleinfo.Action);
                });
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// 初始化系统托盘
        /// </summary>
        private void InitialTray()
        {
            //设置托盘的各个属性
            this.NotifyIcon = new NotifyIcon();
            this.NotifyIcon.BalloonTipText = this.WindowTitle;
            this.NotifyIcon.Text = this.WindowTitle;
            this.NotifyIcon.Icon = Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.NotifyIcon.Visible = true;
            this.NotifyIcon.ShowBalloonTip(2000);

            //this.NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_MouseClick);

            ////设置菜单项
            //System.Windows.Forms.MenuItem menu1 = new System.Windows.Forms.MenuItem("菜单项1");
            //System.Windows.Forms.MenuItem menu2 = new System.Windows.Forms.MenuItem("菜单项2");
            //System.Windows.Forms.MenuItem menu = new System.Windows.Forms.MenuItem("菜单", new System.Windows.Forms.MenuItem[] { menu1 , menu2 });

            ////退出菜单项
            //System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("exit");
            //exit.Click += new EventHandler(exit_Click);

            ////关联托盘控件
            //System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { menu , exit };
            //this.NotifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);
        }
        /// <summary>
        /// 模块加载管理
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="action"></param>
        private void LoadModule(string moduleName, Action action)
        {
            var module = this.ModuleCatalog.Modules.First(m => m.ModuleName == moduleName);

            if (module != null && module.ModuleName == moduleName && module.State == ModuleState.Initialized)
            {
                if (action != null)
                {
                    action.BeginInvoke(null, null);
                }
                return;
            }
            else
            {
                if (module.State == ModuleState.Initializing || module.State == ModuleState.LoadingTypes) return;
                if (action != null && !this.Actions.ContainsKey(moduleName))
                {
                    this.Actions.Add(moduleName, action);
                }
                this.ModuleManager.LoadModule(moduleName);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void OnImportsSatisfied()
        {
            this.ModuleManager.LoadModuleCompleted -= this.ModuleManager_LoadModuleCompleted;
            this.ModuleManager.LoadModuleCompleted += this.ModuleManager_LoadModuleCompleted;
            this.ModuleManager.ModuleDownloadProgressChanged -= this.ModuleManager_ModuleDownloadProgressChanged;
            this.ModuleManager.ModuleDownloadProgressChanged += this.ModuleManager_ModuleDownloadProgressChanged;
        }
        /// <summary>
        /// Handles the LoadModuleProgressChanged event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Composite.Modularity.ModuleDownloadProgressChangedEventArgs"/> instance containing the event data.</param>
        private void ModuleManager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
            Debug.WriteLine(e.TotalBytesToReceive.ToString());
        }
        /// <summary>
        /// Handles the LoadModuleCompleted event of the ModuleManager control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Microsoft.Practices.Composite.Modularity.LoadModuleCompletedEventArgs"/> instance containing the event data.</param>
        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            if (this.Actions.ContainsKey(e.ModuleInfo.ModuleName))
            {
                System.Windows.Application.Current.MainWindow.Dispatcher.BeginInvoke(new Action(() =>
                {
                    this.Actions[e.ModuleInfo.ModuleName].BeginInvoke(null, null);
                }));
            }
            Debug.WriteLine(e.IsErrorHandled.ToString());
        }

        #endregion

        #region interface method

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.NotifyIcon.Visible = false;
            this.NotifyIcon.Dispose();
            this.NotifyIcon = null;
        }

        #endregion
    }
}
