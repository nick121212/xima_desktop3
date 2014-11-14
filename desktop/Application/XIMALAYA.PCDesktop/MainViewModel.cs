using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Shell;
using System.Windows.Threading;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.ViewModel;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Tools.Setting;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop
{
    /// <summary>
    /// mvvm model
    /// </summary>
    [Export(typeof(MainViewModel))]
    public class MainViewModel : NotificationObject, IPartImportsSatisfiedNotification, IDisposable
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
        /// <summary>
        /// 窗体在最前端
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="hWndInsertAfter"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="cx"></param>
        /// <param name="cy"></param>
        /// <param name="uFlags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        private static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        const int SWP_SHOWWINDOW = 0x0040;

        #endregion

        #region 字段

        private string _WindowTitle = string.Empty;
        /// <summary>
        /// 定时器，用于清理内存
        /// </summary>
        private DispatcherTimer ClearMemeryTimer = new DispatcherTimer();
        /// <summary>
        /// 定时器，用于监测指令队列
        /// </summary>
        private DispatcherTimer CheckQueueTimer = new DispatcherTimer();

        #endregion

        #region 属性

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
        /// 托盘服务
        /// </summary>
        public TaskbarIcon NotifyIcon { get; set; }

        #endregion

        #region 构造

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
            this.WindowTitle = GlobalDataSingleton.Instance.SystemTitle;

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
                    //DownloadImage(cover);
                });
                //添加到跳转列表
                this.EventAggregator.GetEvent<JumplistEvent<JumpListEventArgs>>().Subscribe(e =>
                {
                    this.AddKeyToJumpList(e);
                });
            }
            App.Current.Exit += (o, e) =>
            {
                this.Dispose();
            };
            //注册程序错误事件
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler((sender, e) =>
            {
                //MessageBox.Show(e.ExceptionObject.ToString());
                Debug.WriteLine("**********************************************************************");
                Debug.WriteLine("喜马拉雅出现错误：" + DateTime.Now.ToShortDateString());
                Debug.WriteLine("喜马拉雅出现错误：" + e.ExceptionObject.ToString());
                Debug.WriteLine("喜马拉雅出现错误：" + e.IsTerminating.ToString());
                Debug.WriteLine("**********************************************************************");

                //Process.GetCurrentProcess().Kill();
            });
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="notifyIcon"></param>
        /// <param name="mainWindow"></param>
        public void Init(TaskbarIcon notifyIcon, Window mainWindow)
        {
            this.NotifyIcon = notifyIcon;
            this.ClearMemeory();
            this.CheckArgQueue();
        }
        private void CheckArgQueue()
        {
            string arg;
            JumplistEvent<string> jumpEvent = this.EventAggregator.GetEvent<JumplistEvent<string>>();

            this.CheckQueueTimer.Interval = TimeSpan.FromMilliseconds(500);
            this.CheckQueueTimer.Tick += new EventHandler((o, e) =>
            {
                arg = LoadKeyFromMappedFile();
                ClearMappedFile();
                jumpEvent.Publish(arg);
            });

            this.CheckQueueTimer.IsEnabled = true;
        }
        string LoadKeyFromMappedFile()
        {
            try
            {
                using (Stream stream = GlobalDataSingleton.Instance.MemoryMappedFile.CreateViewStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream).ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        void ClearMappedFile()
        {
            try
            {
                using (Stream stream = GlobalDataSingleton.Instance.MemoryMappedFile.CreateViewStream())
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, 0);
                }
            }
            catch { }
        }
        /// <summary>
        /// 定时清除内存
        /// </summary>
        private void ClearMemeory()
        {
            this.ClearMemeryTimer.Interval = TimeSpan.FromSeconds(50);
            this.ClearMemeryTimer.Tick += new EventHandler((o, e) =>
            {
                SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            });
            this.ClearMemeryTimer.IsEnabled = true;
        }
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="url"></param>
        async void DownloadImage(string url)
        {
            try
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
            catch { }
        }
        /// <summary>
        /// 更新UI
        /// </summary>
        /// <param name="downloadTask"></param>
        /// <param name="stream"></param>
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
        /// <summary>
        /// 更改背景色
        /// </summary>
        /// <param name="NewCover"></param>
        void ChangeBackground(BitmapSource NewCover)
        {
            ColorUntil.GetImageColorForBackgroundAsync(NewCover, new ColorUntil.ComputeCompleteCallback((color) =>
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    GlobalDataSingleton.Instance.CurrentSoundCoverColor = color;
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
        /// <summary>
        /// 添加到跳转列表
        /// </summary>
        /// <param name="title"></param>
        /// <param name="arguments"></param>
        private void AddKeyToJumpList(JumpListEventArgs args)
        {
            JumpList jumpList = JumpList.GetJumpList(App.Current);

            if (jumpList == null) jumpList = new JumpList();
            //jumpList.ShowRecentCategory = false;
            //jumpList.ShowFrequentCategory = true;
            foreach (JumpTask jumpItem in jumpList.JumpItems)
            {
                if (jumpItem.Title == args.Title) return;
            }
            JumpTask jumpTask = new JumpTask();
            jumpTask.Title = args.Title;
            jumpTask.Description = jumpTask.Title;
            jumpTask.Arguments = args.Arguments;
            jumpTask.CustomCategory = args.Category;
            jumpTask.WorkingDirectory = Directory.GetCurrentDirectory();
            jumpTask.ApplicationPath = Assembly.GetExecutingAssembly().Location;
            jumpList.JumpItems.Insert(0, jumpTask);
            if (jumpList.JumpItems.Count > 20)
            {
                jumpList.JumpItems.RemoveAt(20);
            }
            //jumpList.JumpItems.RemoveRange(10, 10);
            JumpList.AddToRecentCategory(jumpTask);

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                JumpList.SetJumpList(App.Current, jumpList);
            }));
        }

        #endregion

        #region IDisposable接口

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
            this.ClearMemeryTimer.IsEnabled = false;
            this.ClearMemeryTimer = null;
            XMSetting.Instance.Dispose();
        }

        #endregion
    }
}
