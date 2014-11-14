using System.ComponentModel.Composition;
using CefSharp.Wpf;
using Microsoft.Practices.Prism.Commands;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Common.Events;
using XIMALAYA.PCDesktop.Modules.Passport.Views;
using XIMALAYA.PCDesktop.Tools;
using XIMALAYA.PCDesktop.Untils;

namespace XIMALAYA.PCDesktop.Modules.Passport
{
    /// <summary>
    /// 登录页viewmodel
    /// </summary>
    [Export, PartCreationPolicy(CreationPolicy.Shared)]
    public class LoginViewModel : BaseViewModel
    {
        #region 字段

        private bool _IsThirdPartShow;
        private IWpfWebBrowser _WebBrowser;
        private string _Address;
        private int _CurrentPageIndex;

        #endregion

        #region 属性

        /// <summary>
        /// 第三方登录命令
        /// </summary>
        public DelegateCommand<string> ThirdPartLoginCommand { get; set; }
        public DelegateCommand ShowNormalLogin { get; set; }
        /// <summary>
        /// 第三方登录窗口
        /// </summary>
        public bool IsThirdPartShow
        {
            get
            {
                return _IsThirdPartShow;
            }
            set
            {
                if (value != _IsThirdPartShow)
                {
                    _IsThirdPartShow = value;
                    this.RaisePropertyChanged(() => this.IsThirdPartShow);
                    this.RaisePropertyChanged(() => this.IsThirdPartShow1);
                }
            }
        }

        public bool IsThirdPartShow1
        {
            get
            {
                return !this.IsThirdPartShow;
            }
        }
        /// <summary>
        /// IWpfWebBrowser
        /// </summary>
        public IWpfWebBrowser WebBrowser
        {
            get
            {
                return _WebBrowser;
            }
            set
            {
                if (value != _WebBrowser)
                {
                    _WebBrowser = value;
                    this.RaisePropertyChanged(() => this.WebBrowser);
                }
            }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                if (value != _Address)
                {
                    _Address = value;
                    this.RaisePropertyChanged(() => this.Address);
                }
            }
        }
        /// <summary>
        /// 佔位服务
        /// </summary>
        public int CurrentPageIndex
        {
            get
            {
                return _CurrentPageIndex;
            }
            set
            {
                if (value != _CurrentPageIndex)
                {
                    _CurrentPageIndex = value;
                    this.RaisePropertyChanged(() => this.CurrentPageIndex);
                }
            }
        }

        #endregion

        #region 构造

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginViewModel()
            : base()
        {
            this.Address = "http://www.ximalaya.com";
            //this.IsThirdPartShow = true;
            this.ShowNormalLogin = new DelegateCommand(() =>
            {
                this.IsThirdPartShow = true;
            });
            this.ThirdPartLoginCommand = new DelegateCommand<string>((type) =>
            {
                if (!string.IsNullOrEmpty(type))
                {
                    string url = string.Format(WellKnownUrl.ThirdLoginPath, type.ToString());
                    //this.IsThirdPartShow = false;
                    this.CurrentPageIndex = 1;

                    this.Address = url;
                }
            });
        }

        #endregion

        #region 方法

        /// <summary>
        /// 初始化
        /// </summary>
        public void DoInit()
        {
            //CSharpBrowserSettings settings = new CSharpBrowserSettings()
            //{
            //    CachePath = @"C:\temp\caches",
            //    UserAgent = string.Format("ting-ximalaya_v{0} name/ximalaya os/{1} osName/{2}", GlobalDataSingleton.Instance.Version, OSInfo.Instance.OsInfo.VersionString, OSInfo.Instance.OsInfo.Platform.ToString()),
            //};

            //this.View.ChromeWebBrowser.Initialize(settings);
        }

        #endregion
    }
}
