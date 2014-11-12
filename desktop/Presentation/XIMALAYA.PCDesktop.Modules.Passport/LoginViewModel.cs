using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Sashulin;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Modules.Passport.Views;
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

        private Uri _ThirdPartUri;
        private bool _IsThirdPartShow;

        #endregion

        #region 属性

        /// <summary>
        /// 第三方登录命令
        /// </summary>
        public DelegateCommand<string> ThirdPartLoginCommand { get; set; }
        /// <summary>
        /// 第三方登录地址
        /// </summary>
        public Uri ThirdPartUri
        {
            get
            {
                return _ThirdPartUri;
            }
            set
            {
                if (value != _ThirdPartUri)
                {
                    _ThirdPartUri = value;
                    this.RaisePropertyChanged(() => this.ThirdPartUri);
                }
            }
        }
        /// <summary>
        /// 是否显示第三方登录界面
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
                }
            }
        }
        /// <summary>
        /// 视图
        /// </summary>
        [Import]
        public LoginView View { get; set; }

        #endregion

        #region 构造

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginViewModel()
            : base()
        {
            this.ThirdPartLoginCommand = new DelegateCommand<string>((type) =>
            {
                if (!string.IsNullOrEmpty(type))
                {
                    this.ThirdPartUri = new Uri(string.Format(WellKnownUrl.ThirdLoginPath, type.ToString()));
                    this.IsThirdPartShow = true;

                    CSharpBrowserSettings settings = new CSharpBrowserSettings();
                    //settings.DefaultUrl = System.IO.Directory.GetCurrentDirectory() + "\\cachedbTest.html";
                    //settings.UserAgent = "Mozilla/5.0 (Linux; Android 4.2.1; en-us; Nexus 4 Build/JOP40D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19";
                    settings.CachePath = @"C:\temp\caches";
                    this.View.ChromeWebBrowser.Initialize(settings);
                    this.View.ChromeWebBrowser.OpenUrl("http://www.ximalaya.com/passport/auth/2/authorize?responseJson=true");

                    //this.View.webbrowser.Child = webbrowser;

                    //webbrowser.Url = "http://www.baidu.com";
                }
            });
        }

        #endregion
    }
}
