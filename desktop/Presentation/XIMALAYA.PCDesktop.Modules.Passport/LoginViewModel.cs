using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Threading;
using CefSharp.Wpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.Prism.Commands;
using XIMALAYA.PCDesktop.Common;
using XIMALAYA.PCDesktop.Core.Models.User;
using XIMALAYA.PCDesktop.Core.ParamsModel;
using XIMALAYA.PCDesktop.Core.Services;
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

        private IWpfWebBrowser _WebBrowser;
        private string _Address;
        private int _CurrentPageIndex;
        private string _Account;
        private string _Password;

        #endregion

        #region 命令

        /// <summary>
        /// 第三方登录命令
        /// </summary>
        public DelegateCommand<string> ThirdPartLoginCommand { get; set; }
        /// <summary>
        /// 登录命令
        /// </summary>
        public DelegateCommand DoLoginCommand { get; set; }

        #endregion

        #region 属性

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
                    this.BindBrowserEvent();
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
        /// 当前选中的tab
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
        /// <summary>
        /// 账号名称
        /// </summary>
        public string Account
        {
            get
            {
                return _Account;
            }
            set
            {
                if (value != _Account)
                {
                    _Account = value;
                    this.RaisePropertyChanged(() => this.Account);
                    this.DoLoginCommand.RaiseCanExecuteChanged();
                }
            }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                if (value != _Password)
                {
                    _Password = value;
                    this.RaisePropertyChanged(() => this.Password);
                    this.DoLoginCommand.RaiseCanExecuteChanged();
                }
            }
        }
        /// <summary>
        /// 用户服务
        /// </summary>
        [Import]
        private IUserService UserService { get; set; }

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
                    this.CurrentPageIndex = 1;
                    this.Address = string.Format(WellKnownUrl.ThirdLoginPath, type.ToString());
                }
            });
            this.DoLoginCommand = new DelegateCommand(() =>
            {
                this.IsWaiting = true;
                this.UserService.DoLogin(obj =>
                {
                    Dictionary<string, object> dic = null;
                    this.IsWaiting = false;
                    Application.Current.Dispatcher.Invoke(new Action(delegate
                    {
                        dic = this.CheckData(obj.ToString());

                        if (dic == null || !dic.ContainsKey("ret")) return;
                        if (dic["ret"].ToString() != "0")
                        {
                            DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "登录中...", dic["msg"].ToString());
                        }
                    }));
                }, new LoginParam
                {
                    Account = this.Account,
                    Password = this.Password,
                    Remember = true,
                    DeviceToken = "none"
                });
            }, () =>
            {
                return !string.IsNullOrWhiteSpace(this.Account) && !string.IsNullOrWhiteSpace(this.Password) && !this.IsWaiting;
            });

            this.Account = "tinytiny8@163.com";
            this.Password = "111111";
            this.Address = "empty.html";
        }

        #endregion

        #region 方法

        /// <summary>
        /// 绑定浏览器事件
        /// </summary>
        private void BindBrowserEvent()
        {
            if (this.WebBrowser != null)
            {
                this.WebBrowser.FrameLoadStart += WebBrowser_FrameLoadStart;
                this.WebBrowser.FrameLoadEnd += WebBrowser_FrameLoadEnd;
                //this.WebBrowser.IsBrowserInitialized();
            }
        }

        void WebBrowser_FrameLoadStart(object sender, CefSharp.FrameLoadStartEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                this.IsWaiting = true;
            }));
        }
        /// <summary>
        /// 验证数据
        /// </summary>
        /// <param name="result"></param>
        private Dictionary<string, object> CheckData(string result)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<string, object> dic = js.Deserialize<Dictionary<string, object>>(result);

                if (!dic.ContainsKey("ret")) return null;

                switch (dic["ret"].ToString())
                {
                    case "0"://登录成功
                        HttpWebRequestSingleton.Instance.SetCookies("1&_token", string.Format("{0}&{1}", dic["uid"].ToString(), dic["token"].ToString()));
                        this.GetLoginInfo();
                        this.CurrentPageIndex = 0;
                        break;
                    case "-1"://失败
                        this.CurrentPageIndex = 0;
                        break;
                }

                return dic;
            }
            catch
            {

            }

            return null;
        }
        /// <summary>
        /// 浏览器加载完毕后事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void WebBrowser_FrameLoadEnd(object sender, CefSharp.FrameLoadEndEventArgs e)
        {
            string result = await this.WebBrowser.GetTextAsync();

            Application.Current.Dispatcher.Invoke(new Action(delegate
            {
                this.IsWaiting = false;
                this.WebBrowser.Focus();
                if (result.IndexOf('{') == 0)
                {
                    this.CheckData(result);
                }
            }));
        }
        /// <summary>
        /// 获取登录用户信息
        /// </summary>
        private async void GetLoginInfo()
        {
            var controller = await DialogManager.ShowProgressAsync(Application.Current.MainWindow as MetroWindow, "登录中...", "获取登录用户信息");

            this.UserService.GetLoginUserInfo(async result =>
            {
                UserData userInfo = result as UserData;

                await controller.CloseAsync();

                if (userInfo.Ret == 0)
                {
                    await Application.Current.Dispatcher.InvokeAsync(async () =>
                    {
                        await DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "登录中...", "登录成功！");
                    }, DispatcherPriority.Background);
                }
                else
                {
                    await DialogManager.ShowMessageAsync(Application.Current.MainWindow as MetroWindow, "登录中...", userInfo.Message);
                }

            }, new BaseParam());
        }

        #endregion
    }
}
