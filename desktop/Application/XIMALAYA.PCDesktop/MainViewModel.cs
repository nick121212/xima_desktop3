using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Events;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Player;
using XIMALAYA.PCDesktop.Tools.Setting;
using XIMALAYA.PCDesktop.Tools.Themes;
using XIMALAYA.PCDesktop.Tools.Untils;

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
        /// <summary>
        /// 播放相关
        /// </summary>
        public BassEngine BassEngine
        {
            get
            {
                return PlayerSingleton.Instance;
            }
        }
        /// <summary>
        /// 简版界面
        /// </summary>
        public List<TileSet> TileSets { get; set; }

        #endregion

        #region command

        /// <summary>
        /// 显示隐藏界面
        /// </summary>
        public DelegateCommand ShowOrHideCommand { get; set; }

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
            this.Actions = new Dictionary<string, Action>();
            this.ModuleManager = moduleManager;
            this.ModuleCatalog = moduleCatalog;
            this.EventAggregator = eventAggregator;
            this.WindowTitle = @"喜马拉雅-听我想听";
            this.ShowOrHideCommand = new DelegateCommand(() =>
            {
                if (Application.Current.MainWindow.Visibility == Visibility.Visible)
                {
                    Application.Current.MainWindow.Visibility = Visibility.Hidden;
                }
                else
                {
                    Application.Current.MainWindow.Visibility = Visibility.Visible;
                }
            });
            //订阅加载模块事件
            if (this.EventAggregator != null)
            {
                //注册加载模块事件
                this.EventAggregator.GetEvent<ModulesManagerEvent>().Subscribe(moduleinfo =>
                {
                    this.LoadModule(moduleinfo.ModuleName, moduleinfo.Action);
                });


                //注册更换cover图事件
                this.EventAggregator.GetEvent<ChangeCoverEvent>().Subscribe(cover =>
                {
                    DownloadImage(cover);
                });
            }
            //注册程序错误事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((sender, e) =>
            {
                Debug.WriteLine("**********************************************************************");
                Debug.WriteLine("喜马拉雅出现错误：" + DateTime.Now.ToShortDateString());
                Debug.WriteLine("**********************************************************************");

                Process.GetCurrentProcess().Kill();
            });
        }

        #endregion

        #region methods

        async void DownloadImage(string url)
        {
            var request = WebRequest.Create(url);
            using (var response = await request.GetResponseAsync())
            using (var destStream = new MemoryStream())
            {
                var responseStream = response.GetResponseStream();
                var downloadTask = responseStream.CopyToAsync(destStream);
                RefreshUI(downloadTask, destStream);
                await downloadTask;
            }
        }
        async void RefreshUI(Task downloadTask, MemoryStream stream)
        {
            await Task.WhenAny(downloadTask, Task.Delay(1000));

            var data = stream.ToArray();
            var tmpStream = new MemoryStream(data);
            var bmp = new BitmapImage();

            bmp.BeginInit();
            bmp.StreamSource = tmpStream;
            bmp.EndInit();

            if (downloadTask.IsCompleted)
            {
                ChangeBackground(bmp);
            }

        }
        void ChangeBackground(BitmapSource NewCover)
        {
            ColorFunctions.GetImageColorForBackgroundAsync(NewCover, new ColorFunctions.ComputeCompleteCallback((color) =>
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    CommandSingleton.Instance.CurrentSoundCoverColor = color;
                }));
            }));
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
            this.Actions.Clear();
            this.Actions = null;
            this.ModuleManager = null;
            this.ModuleCatalog = null;
            this.EventAggregator = null;
        }

        #endregion
    }
}
